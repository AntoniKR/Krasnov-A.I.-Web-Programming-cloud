Chart.register(ChartDataLabels);    // плагин для отображения долей

fetch('/Stocks/GetChartT')   // Круговая Диаграмма по тикерам
    .then(response => response.json())
    .then(data => {
        const ctx = document.getElementById('TickerPie');
        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: data.map(d => d.label),
                datasets: [{
                    data: data.map(d => d.total),
                }]
            },
            options: {
                responsive: false,
                maintainAspectRatio: false
            }
        });
    });

fetch('/StocksUSD/GetChartT')   // Круговая Диаграмма по тикерам
    .then(response => response.json())
    .then(data => {
        const ctx = document.getElementById('TickerPie');
        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: data.map(d => d.label),
                datasets: [{
                    data: data.map(d => d.total),
                }]
            },
            options: {
                responsive: false,
                maintainAspectRatio: false
            }
        });
    });

fetch('/Stocks/GetChartC')   // Столбчатая Диаграмма  по странам
    .then(response => response.json())
    .then(data => {
        const ctx = document.getElementById('CountryPie');
        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: data.map(d => d.label),
                datasets: [{
                    label: "Сумма в рублях",
                    data: data.map(d => d.total),
                }]
            },
            options: {
                responsive: false,
                maintainAspectRatio: false
            }
        });
    })

fetch('/Home/GetRateContr')   // Запрос к курсу
    .then(response => response.json())
    .then(rate => {
        document.getElementById("RateUSD").innerHTML = rate.toFixed(2) + " ₽";
    });

fetch('/Home/GetAssetsChart')   // Круговая Диаграмма с общей суммой активов
    .then(response => response.json())
    .then(data => {
        const totalSum = data.reduce((sum, item) => sum + item.total, 0);

        const ctx = document.getElementById('SummPie');
        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: data.map(d => d.label),
                datasets: [{
                    data: data.map(d => d.total),
                }]
            },
            options: {
                responsive: false,
                maintainAspectRatio: false
            }
        });
        const ctxBar = document.getElementById('SummBar');
        new Chart(ctxBar, {
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
                        formatter: function (value) {
                            return value + "%";
                        },
                        font: {
                            weight: 'bold'
                        },
                        color: '#000'
                    }
                },
            },
            
        });
        document.getElementById("Summ").innerHTML = totalSum.toLocaleString("ru-RU", {
            style: "currency",
            currency: "RUB"
        });

    });
fetch('/Crypto/GetChartTicker')   // Круговая Диаграмма КРИПТА
    .then(response => response.json())
    .then(data => {
        const ctx = document.getElementById('CryptoPie');
        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: data.map(d => d.label),
                datasets: [{
                    data: data.map(d => d.total),
                }]
            },
            options: {
                responsive: false,
                maintainAspectRatio: false
            }
        });
    });
fetch('/Metals/GetChartT')   // Круговая Диаграмма металлы
    .then(response => response.json())
    .then(data => {
        const ctx = document.getElementById('MetalsPie');
        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: data.map(d => d.label),
                datasets: [{
                    data: data.map(d => d.total),
                }]
            },
            options: {
                responsive: false,
                maintainAspectRatio: false
            }
        });
    });