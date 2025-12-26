INSERT INTO "public"."Users" ("Id", "Username", "PasswordHash") VALUES
(1, 'admin', '$2a$11$uS6yqVFHoX6muLrSQR46yOsnZguqIVyQ5tUXO9vsxplF8xvzlKpc6'),
(2, 'Anton', '$2a$11$ujCDJ3ihHLd5lOzxTq0tDujnpzFw3mB2.ihxCstAhMKOulmnwl.dS'),
(3, 'NewUser', '$2a$11$xjXEJz9SP7vIJnvT3NjN6O/XU1bNpazLQ.55j4Z0BRKvPyccwGgy.'),
(4, 'Antoxa', '$2a$11$fMUEOxuaoOYV3t/qw6h7PO/9mH0uEbnKGOWYmO8MYuOoJ0Rw6xy.m'),
(5, 'LOH', '$2a$11$q9mZdPulAr3hUHSwyxYMd.hpy63D/Zvg9gIRdWVX.pJt.2bn4VmMq'),
(6, 'test123', '$2a$11$67k/kwW0yjrPvEmCO7LWIuBIeZYvJVE68dwf/Bg4VlpuigvX/KOVe'),
(7, 'kessy_meow', '$2a$11$rzkkLV9rWIRLU5523KzveuzyJiGwa/meBGPjp6PCs4xVrpce.VC0i'),
(8, 'svl', '$2a$11$WNo72JA1DXlx7skqeATQA.puzilvFZQGfOdGBALbxoYmRuRSbQe7C'),
(9, 'svl', '$2a$11$A/tdgj7jlxJQBUzYLKkcNeyO.m82owJhM1GrQEqxPCxaffu0/6L/y'),
(10, 'login', '$2a$11$FMCg3.9UqePCZsLnUr86POzb9W1hFVnAITpIxoTUSAP9GPgKJR5/C');
INSERT INTO "public"."Cryptos" ("Id", "Ticker", "NameCrypto", "Price", "AmountCrypto", "SumCrypto", "SumCryptoToRuble", "DateAddStock", "UserId") VALUES
(21, 'PRCLUSDT', 'Parcel', 0.35, 42.85, 14.9975, 1214.54854150, '2025-10-20 15:01:09.416836+03', 1),
(22, 'STRKUSDT', 'Starknet', 0.44, 34.09, 14.9996, 1214.71860664, '2025-10-20 15:01:53.235482+03', 1),
(23, 'AXLUSDT', 'Axelar', 0.65, 23.05, 14.9825, 1213.33379050, '2025-10-20 15:02:32.874412+03', 1),
(24, 'JASMYUSDT', 'JasmyCoin', 0.0194, 515.46, 9.999924, 809.8278452616, '2025-10-20 15:03:02.779516+03', 1),
(25, 'DOGEUSDT', 'Dogecoin', 0.36, 27.67, 9.9612, 806.69184408, '2025-10-20 15:03:58.005606+03', 1),
(26, 'SHIBUSDT', 'Shiba', 0.0000245, 612883.4, 15.01564330, 1216.017847621220, '2025-10-20 15:04:31.769037+03', 1),
(27, 'HBARUSDT', 'Hedera', 0.32, 46.82, 14.9824, 1213.32569216, '2025-10-20 15:05:25.523567+03', 1),
(28, 'SNXUSDT', 'Syntexx', 1.5, 6.65, 9.975, 807.8094150, '2025-10-20 15:05:55.524279+03', 1),
(29, 'UNIUSDT', 'Uniswap', 6, 6.99, 41.94, 3396.443796, '2025-10-20 15:06:32.573719+03', 1),
(30, 'ADAUSDT', 'Cardano', 0.5, 99.9, 49.95, 4045.120830, '2025-10-20 15:07:09.187043+03', 1),
(31, 'XRPUSDT', 'Ripple', 0.5, 49.95, 24.975, 2022.5604150, '2025-10-20 15:07:33.440782+03', 1),
(36, 'BTCUSDE', 'хз', 99999999, 1, 99999999, 7902459920.98, '2025-11-24 13:59:30.986974+03', 5),
(38, 'dfg', 'dhhh', 355, 4, 1420, 109067.93, '2025-12-09 21:59:44.221312+03', 10),
(39, 'SOLUSDT', 'Solanaa', 130, 0.384, 49.920, 3960.64, '2025-12-12 09:30:30.01707+03', 1);
INSERT INTO "public"."Currencies" ("Id", "NameCurrency", "CharCode", "Price", "AmountCurrency", "SumCurrencyToRuble", "DateAddStock", "UserId") VALUES
(3, 'Австралийский доллар', 'AUD', 50, 200, 10000, '2025-10-23 16:20:24.828181+03', 1),
(4, 'Гонконгский доллар', 'HKD', 15, 200, 3000, '2025-10-24 10:37:03.268252+03', 1),
(6, 'СДР (специальные права заимствования)', 'XDR', 50, 1, 50, '2025-11-06 15:02:43.859225+03', 1),
(12, 'Канадский доллар', 'CAD', 61.5, 200, 12300.0, '2025-11-25 09:25:01.918696+03', 7),
(13, 'Доллар США', 'USD', 82.8, 400, 33120.0, '2025-11-25 09:28:50.87051+03', 7),
(14, 'Доллар США', 'USD', 80.05, 10, 800.50, '2025-11-25 09:30:33.383943+03', 1);
INSERT INTO "public"."Metals" ("Id", "NameMetal", "Price", "AmountMetal", "SumMetals", "DateAddStock", "UserId") VALUES
(1, 'Серебро', 60, 200, 12000, '2025-11-25 08:45:45.547737+03', 1),
(7, 'Платина', 3000, 1, 3000, '2025-10-22 13:10:47.811603+03', 1),
(9, 'Золото', 3000, 2, 6000, '2025-12-24 09:40:38.040608+03', 1);
INSERT INTO "public"."PlatformStartups" ("Id", "NamePlatform", "AmountCompanies", "SumOfStartups", "DateAddStock", "UserId") VALUES
(1, 'Zorko', 2, 16482, '2025-11-24 13:54:29.870086+03', 1),
(5, 'BrainBox', 3, 48223.22, '2025-11-12 15:34:26.550626+03', 1);
INSERT INTO "public"."RealEstates" ("Id", "TypeEstate", "CityEstate", "Price", "AmountEstate", "SumEstate", "DateAddStock", "UserId") VALUES
(1, 'Квартира', 'Нижний Новгород', 1500000, 1, 1500000, '2025-11-19 13:38:04.623699+03', 1),
(4, 'Дачный участок', 'Москва', 100000, 1, 100000, '2025-11-19 13:56:45.662379+03', 1);
INSERT INTO "public"."Stocks" ("Id", "Ticker", "NameCompany", "Price", "AmountStock", "SumStocks", "DateAddStock", "UserId") VALUES
(8, 'ROSN', 'Роснефть', 325, 5, 1625, '2025-09-23 15:21:05.400095+03', 1),
(16, 'SBER', 'Сбербанк', 300, 10, 3000, '2025-09-24 15:35:01.985556+03', 1),
(19, 'Hype', 'Hyper', 216, 122, 26352, '2025-09-25 15:47:16.304204+03', 2),
(20, 'GEMI', 'Gemini', 35, 10, 350, '2025-09-25 15:47:38.0503+03', 2),
(21, 'T', 'тИНЬКОФФ', 3000, 3, 9000, '2025-09-29 10:50:16.146821+03', 4),
(22, 'MTSS', 'мтс', 180, 40, 7200, '2025-10-01 11:29:17.396433+03', 1),
(26, 'LKOH', 'луКОЛИЛ', 5600.22, 6, 33601.32, '2025-12-12 10:39:32.962931+03', 1);
INSERT INTO "public"."StocksUSD" ("Id", "Ticker", "NameCompany", "Price", "AmountStock", "SumStocks", "SumStocksToRuble", "DateAddStock", "UserId") VALUES
(1, 'GEMI', 'GEMINI SPACE STATION', 28, 30, 840, 68457.23, '2025-10-02 13:24:41.646167+03', 1),
(5, 'SGFF', NULL, 12, 12, 144, 11758.31, '2025-10-23 11:02:08.227785+03', 1);
INSERT INTO "public"."Transports" ("Id", "TypeTransport", "NameTransport", "Price", "YearOfTransport", "DateAddStock", "UserId") VALUES
(1, 'Мотоцикл', 'МОТОО', 238887, 2007, '2025-12-01 14:19:31.405555+03', 1),
(3, 'Машина', 'dfgdfg', 34343, 2025, '2025-12-01 14:24:11.684933+03', 1),
(4, 'Мотоцикл', 'Suzuki', 1234567, 2010, '2025-12-01 14:27:13.29466+03', 1),
(5, 'Машина', 'wwer', 1536485, 2025, '2025-12-01 14:27:31.93643+03', 1);
INSERT INTO "public"."Startups" ("Id", "NameCompany", "NamePlatform", "Price", "AmountStock", "SumStocks", "DateAddStock", "UserId", "PlatformStartupId") VALUES
(10, 'Qeepl', 'BrainBox', 121, 100, 12100, '2025-11-12 15:34:48.161103+03', 1, 5),
(12, 'VOX', 'BrainBox', 120, 300, 36000, '2025-11-17 10:05:28.032721+03', 1, 5),
(13, 'Hyper', 'Zorko', 216, 10, 2160, '2025-11-24 13:54:47.983702+03', 1, 1),
(14, 'йкйу', 'Zorko', 434, 33, 14322, '2025-11-25 08:40:08.928186+03', 1, 1),
(15, 'цук', 'BrainBox', 123.22, 1, 123.22, '2025-11-25 08:46:32.447673+03', 1, 5);
