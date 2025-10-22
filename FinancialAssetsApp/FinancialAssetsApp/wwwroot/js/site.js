// Функция для безопасного создания Chart
const charts = {}; // глобальный объект для хранения графиков

function createChart(canvasId, config) {
    const ctx = document.getElementById(canvasId);
    if (!ctx) return;

    // Если на этом canvas уже есть Chart — удаляем его
    if (charts[canvasId]) {
        charts[canvasId].destroy();
    }

    charts[canvasId] = new Chart(ctx, config);
}


// Функция для безопасного fetch + json
async function fetchJson(url) {
    try {
        const res = await fetch(url);
        if (!res.ok) throw new Error(res.statusText);
        const json = await res.json();
        return json;
    } catch (err) {
        console.error(`Ошибка при fetch ${url}:`, err);
        return null;
    }
}

//Изменение точки на запятую
document.addEventListener("DOMContentLoaded", () => {   
    document.querySelectorAll('input[name="Price"], input[name="AmountCrypto"]').forEach(input => {
        input.addEventListener("input", () => {
            input.value = input.value.replace(".", ",");
        });
    });
});





// Круговая диаграмма по тикерам RUB
fetchJson('/Stocks/GetChartT').then(data => {
    if (!data) return;
    createChart('TickerPie', {
        type: 'pie',
        data: {
            labels: data.map(d => d.label),
            datasets: [{ data: data.map(d => d.total) }]
        },
        options: { responsive: false, maintainAspectRatio: false }
    });
});

// Круговая диаграмма по тикерам иностранных компаний
fetchJson('/StocksUSD/GetChartT').then(data => {
    if (!data) return;
    createChart('TickerPieUSD', {
        type: 'pie',
        data: {
            labels: data.map(d => d.label),
            datasets: [{ data: data.map(d => d.total) }]
        },
        options: { responsive: false, maintainAspectRatio: false }
    });
});

// Курс USD
/*fetchJson('/Home/GetRateContr').then(rate => {
    if (rate != null) {
        const el = document.getElementById("RateUSD");
        if (el) el.innerHTML = rate.toFixed(2) + " ₽";
    }
});*/
async function getUsdRate() {
    try {
        const rate = await fetchJson('/Home/GetRateContr');
        if (rate != null) {
            const el = document.getElementById("RateUSD");
            if (el) el.innerHTML = rate.toFixed(2) + " ₽";
            return rate;
        }
    } catch (err) {
        console.error("Ошибка получения курса доллара:", err);
        return null;
    }
}
// Круговая диаграмма с общей суммой активов
fetchJson('/Home/GetAssetsChart').then(data => {
    if (!data) return;
    const totalSum = data.reduce((sum, item) => sum + item.total, 0);

    createChart('SummPie', {
        type: 'pie',
        data: {
            labels: data.map(d => d.label),
            datasets: [{ data: data.map(d => d.total) }]
        },
        options: { responsive: false, maintainAspectRatio: false }
    });

    createChart('SummBar', {
        type: 'bar',
        data: {
            labels: data.map(d => d.label),
            datasets: [{
                label: "Доля в портфеле %",
                data: data.map(d => ((d.total / totalSum) * 100).toFixed(2)),
            }]
        },
        options: {
            responsive: false,
            maintainAspectRatio: false,
            plugins: {
                datalabels: {
                    anchor: 'end',
                    align: 'top',
                    offset: -2,
                    formatter: v => v + "%",
                    font: { weight: 'bold' },
                    color: '#000'
                }
            }
        }
    });

    const summEl = document.getElementById("Summ");
    if (summEl) summEl.innerHTML = totalSum.toLocaleString("ru-RU", {
        style: "currency", currency: "RUB"
    });
});

// Круговая диаграмма Крипта
fetchJson('/Crypto/GetChartTicker').then(data => {
    if (!data) return;
    createChart('CryptoPie', {
        type: 'pie',
        data: {
            labels: data.map(d => d.label),
            datasets: [{ data: data.map(d => d.total) }]
        },
        options: { responsive: false, maintainAspectRatio: false }
    });
});

// Круговая диаграмма покупки Металлы
fetchJson('/Metals/GetChartT').then(data => {
    if (!data) return;
    createChart('PurchasePie', {
        type: 'pie',
        data: {
            labels: data.map(d => d.label),
            datasets: [{ data: data.map(d => d.total) }]
        },
        options: { responsive: false, maintainAspectRatio: false }
    });
});

// Список тикеров для крипты
document.addEventListener("DOMContentLoaded", async () => {
    const tickerInput = document.getElementById("TickerInput");
    if (!tickerInput) return;

    const json = await fetchJson("https://api.bybit.com/v5/market/tickers?category=spot");
    if (json && json.retCode === 0 && json.result?.list) {
        const tickers = json.result.list.map(x => x.symbol);
        $(tickerInput).autocomplete({ source: tickers, minLength: 1 });
    }
});

// Текущие цены крипты
document.addEventListener("DOMContentLoaded", async () => {

    const rateUSD = await getUsdRate(); //Для курса доллара и текущей стоимости крипты
    const chartDataCurrent = [];    //Для построения диаграммы
    const rows = document.querySelectorAll("tr[data-cryptos]");  //для графика
    let count = 0;  //Счетчик для построения графика

    document.querySelectorAll("tr[data-cryptos]").forEach(row => {

        const symbol = row.getAttribute("data-cryptos");              //этот блок объявляет переменные
        const currPriceCell = row.querySelector(".current-price");    //для подсчета изменений
        const changeSumCell = row.querySelector(".change-sum");       //в портфеле
        const currentSumCell = row.querySelector(".current-sum");
        const changePriceCell = row.querySelector(".change-price");
        const changeSumRUBCell = row.querySelector(".change-sumRUB");
        const currentSumRUBCell = row.querySelector(".current-sumRUB");

        if (!symbol || !currPriceCell || !changePriceCell) return;

        const purchasePrice = parseFloat(row.cells[2].textContent.replace("$", "")
            .replace(",", ".").trim()); //выделяем цену покупки и меняем запятую на точку
        const amountCrypto = parseFloat(row.cells[5].textContent.replace("$", "").replace(",", ".").trim()); //выделяем количество для вычисления изменения суммы
        const sumRUB = parseFloat(row.cells[9].textContent.replace("₽", "").replace(",", ".").trim()); //выделяем количество для вычисления изменения суммы

        let decimals;   //Для разряда числа
        if (purchasePrice >= 1) decimals = 2;
        else if (purchasePrice >= 0.01) decimals = 3;
        else decimals = 7;

        fetch(`/Crypto/PriceCrypto?symbol=${encodeURIComponent(symbol)}`)
            .then(res => res.json())
            .then(price => {
                const sumCrypto = parseFloat(row.cells[6].textContent.replace("$", "").replace(",", ".").trim()); // Выделяем сумму покупки и меняем запятую на точку
                row.cells[2].textContent = purchasePrice.toFixed(decimals) + " $";
                if (!price || isNaN(price)) {   // Если тикера крипты нет на Bybit, то выдаем данные ниже
                    currPriceCell.textContent = "нет данных";
                    changePriceCell.textContent = "-";
                    changeSumCell.textContent = "-";                    
                    currentSumCell.textContent = sumCrypto.toFixed(decimals) + " $";
                    changeSumRUBCell.textContent = "-";                                     

                    const currentSum = purchasePrice * amountCrypto; //Текущая стоимость крипты
                    const currentSumRUB = currentSum * rateUSD; //Текущая стоимость в рублях
                    currentSumRUBCell.textContent = currentSumRUB.toFixed(2) + " ₽";

                    const changeProcentSumRUB = ((currentSumRUB - sumRUB) / sumRUB) * 100;  // Изменение в процентах
                    const changeFormatSumRUB = (currentSumRUB - sumRUB).toFixed(2) + " ₽" + " (" + changeProcentSumRUB.toFixed(2) + " %)";
                    changeSumRUBCell.textContent = changeFormatSumRUB;
                    changeSumRUBCell.style.color = changeProcentSumRUB >= 0 ? "green" : "red";   // Цвет изменения суммы      
                    chartDataCurrent.push({ label: symbol, value: currentSumRUB });
                }
                else {                  
                    const currentPrice = parseFloat(price);
                    currPriceCell.textContent = parseFloat(price).toFixed(2) + " $";     //Текущая цена криптовалюты  
                    
                    const changeProcent = ((currentPrice - purchasePrice) / purchasePrice) * 100;  // Изменение в процентах
                    const changeFormatPrice = (currentPrice - purchasePrice).toFixed(decimals) + " $" + " (" + changeProcent.toFixed(2) + " %)";   // Для отображения изменения цены и в процентах
                    changePriceCell.textContent = changeFormatPrice;    //Изменение в долларах
                    changePriceCell.style.color = changeProcent >= 0 ? "green" : "red"; // Цвет процентов              

                    //Изменение стоимости 
                    const currentSum = currentPrice * amountCrypto; //Текущая стоимость крипты
                    currentSumCell.textContent = currentSum.toFixed(decimals) + " $";
                    const changeFormatSum = (currentSum - sumCrypto).toFixed(2) + " $" + " (" + changeProcent.toFixed(2) + " %)";
                    changeSumCell.textContent = changeFormatSum;
                    changeSumCell.style.color = changeProcent >= 0 ? "green" : "red";   // Цвет изменения суммы

                    //Изменение стоимости в рублях
                    const currentSumRUB = currentSum * rateUSD; //Текущая стоимость в рублях
                    currentSumRUBCell.textContent = currentSumRUB.toFixed(2) + " ₽";

                    const changeProcentSumRUB = ((currentSumRUB - sumRUB) / sumRUB) * 100;  // Изменение в процентах
                    const changeFormatSumRUB = (currentSumRUB - sumRUB).toFixed(2) + " ₽" + " (" + changeProcentSumRUB.toFixed(2) + " %)";
                    changeSumRUBCell.textContent = changeFormatSumRUB;
                    changeSumRUBCell.style.color = changeProcentSumRUB >= 0 ? "green" : "red";   // Цвет изменения суммы

                    chartDataCurrent.push({ label: symbol, value: currentSumRUB });
                }                   
            })
            .catch(() => {
                currPriceCell.textContent = "ошибка";
                changePriceCell.textContent = "-";
            })
            .finally(() => {    //Когда обработали строку, проверяем, можно ли строить диаграмму
                
                count++;
                if (count === rows.length) {
                    createChart('CurrentCryptoPie', {
                        type: 'pie',
                        data: {
                            labels: chartDataCurrent.map(d => d.label),
                            datasets: [{ data: chartDataCurrent.map(d => d.value) }]
                        },
                        options: { responsive: true, maintainAspectRatio: false }
                    });
                }
            });  
                
    });
});

// Текущие цены и диаграмма Металлы
document.addEventListener("DOMContentLoaded", () => {

    const chartDataCurrent = [];    //Для построения диаграммы
    const rows = document.querySelectorAll("tr[data-metals]");  //для графика
    let count = 0;  //Счетчик для построения графика
    document.querySelectorAll("tr[data-metals]").forEach(row => {

        const symbol = row.getAttribute("data-metals");             //этот блок объявляет переменные
        const currPriceCell = row.querySelector(".current-price");  //для подсчета изменений
        const changeSumCell = row.querySelector(".change-sum");     //в портфеле
        const currentSumCell = row.querySelector(".current-sum");

        if (!symbol || !currPriceCell) return;

        
        const amountMetal = parseFloat(row.cells[3].textContent.replace(",", ".").trim()); //выделяем количество для вычисления изменения суммы
        let decimals = 2;   // Для разрядности чисел

        fetch(`/Metals/PriceMetal?nameMetal=${encodeURIComponent(symbol)}`)
            .then(res => res.text())
            .then(price => {
                //console.log(price);
                const sumMetal = parseFloat(row.cells[4].textContent.replace("₽", "").replace(",", ".").trim()); // Выделяем сумму покупки и меняем запятую на точку

                const currentPrice = parseFloat(price.replace(",", "."));
                currPriceCell.textContent = parseFloat(currentPrice).toFixed(decimals) + " ₽"; //Текущая цена металла             

                //Изменение стоимости 
                const currentSum = currentPrice * amountMetal; //Текущая стоимость крипты
                currentSumCell.textContent = currentSum.toFixed(decimals) + " ₽";
                const changeProcent = ((currentSum - sumMetal) / sumMetal) * 100;  // Изменение в процентах
                const changeFormatSum = (currentSum - sumMetal).toFixed(2) + " ₽" + " (" + changeProcent.toFixed(2) + " %)";
                changeSumCell.textContent = changeFormatSum;
                changeSumCell.style.color = changeProcent >= 0 ? "green" : "red";   // Цвет изменения суммы

                chartDataCurrent.push({ label: symbol, value: currentSum });
            })
            .catch(() => {  //Ловим ошибку
                currPriceCell.textContent = "ошибка";
            })
            .finally(() => {    //Когда обработали строку, проверяем, можно ли строить диаграмму
                count++;
                if (count === rows.length) {
                    createChart('CurrentPricePie', {
                        type: 'pie',
                        data: {
                            labels: chartDataCurrent.map(d => d.label),
                            datasets: [{ data: chartDataCurrent.map(d => d.value) }]
                        },
                        options: { responsive: true, maintainAspectRatio: false }
                    });
                }
            });    
    });

});
