INSERT INTO "public"."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES
('20251124071958_Initial', '9.0.9');
INSERT INTO "public"."Startups" ("Id", "NameCompany", "NamePlatform", "Price", "AmountStock", "SumStocks", "DateAddStock", "UserId", "PlatformStartupId") VALUES
(10, 'Qeepl', 'BrainBox', 121, 100, 12100, '2025-11-12 15:34:48.161103+03', 1, 5),
(12, 'VOX', 'BrainBox', 120, 300, 36000, '2025-11-17 10:05:28.032721+03', 1, 5);
INSERT INTO "public"."Stocks" ("Id", "Ticker", "NameCompany", "Price", "DateAddStock", "AmountStock", "SumStocks", "UserId") VALUES
(8, 'ROSN', 'Роснефть', 325, '2025-09-23 15:21:05.400095+03', 5, 1625, 1),
(16, 'SBER', 'Сбербанк', 300, '2025-09-24 15:35:01.985556+03', 10, 3000, 1),
(19, 'Hype', 'Hyper', 216, '2025-09-25 15:47:16.304204+03', 122, 26352, 2),
(20, 'GEMI', 'Gemini', 35, '2025-09-25 15:47:38.0503+03', 10, 350, 2),
(21, 'T', 'тИНЬКОФФ', 3000, '2025-09-29 10:50:16.146821+03', 3, 9000, 4),
(22, 'MTSS', 'мтс', 180, '2025-10-01 11:29:17.396433+03', 40, 7200, 1),
(26, 'LKOH', 'луКОЛИЛ', 5600.22, '2025-10-23 10:29:45.609552+03', 3, 16800.66, 1);
INSERT INTO "public"."Metals" ("Id", "NameMetal", "Price", "AmountMetal", "SumMetals", "DateAddStock", "UserId") VALUES
(4, 'Серебро', 60, 200, 12000, '2025-10-01 15:42:09.617598+03', 1),
(7, 'Платина', 3000, 1, 3000, '2025-10-22 13:10:47.811603+03', 1),
(8, 'Палладий', 7777, 5, 38885, '2025-10-22 15:57:01.06333+03', 1),
(9, 'Золото', 2916.67, 6, 17500.02, '2025-11-12 14:54:39.379642+03', 1);
INSERT INTO "public"."RealEstates" ("Id", "TypeEstate", "Price", "AmountEstate", "SumEstate", "DateAddStock", "UserId", "CityEstate") VALUES
(1, 'Квартира', 1500000, 1, 1500000, '2025-11-19 13:38:04.623699+03', 1, 'Нижний Новгород'),
(4, 'Дачный участок', 100000, 1, 100000, '2025-11-19 13:56:45.662379+03', 1, 'Москва');
INSERT INTO "public"."Users" ("Id", "Username", "PasswordHash") VALUES
(1, 'admin', '$2a$11$uS6yqVFHoX6muLrSQR46yOsnZguqIVyQ5tUXO9vsxplF8xvzlKpc6'),
(2, 'Anton', '$2a$11$ujCDJ3ihHLd5lOzxTq0tDujnpzFw3mB2.ihxCstAhMKOulmnwl.dS'),
(3, 'NewUser', '$2a$11$xjXEJz9SP7vIJnvT3NjN6O/XU1bNpazLQ.55j4Z0BRKvPyccwGgy.'),
(4, 'Antoxa', '$2a$11$fMUEOxuaoOYV3t/qw6h7PO/9mH0uEbnKGOWYmO8MYuOoJ0Rw6xy.m');
INSERT INTO "public"."StocksUSD" ("Id", "Ticker", "NameCompany", "Price", "AmountStock", "SumStocks", "SumStocksToRuble", "DateAddStock", "UserId") VALUES
(1, 'GEMI', 'GEMINI SPACE STATION', 28, 30, 840, 68457.23, '2025-10-02 13:24:41.646167+03', 1),
(5, 'SGFF', NULL, 12, 12, 144, 11758.31, '2025-10-23 11:02:08.227785+03', 1);
INSERT INTO "public"."Currencies" ("Id", "NameCurrency", "CharCode", "Price", "AmountCurrency", "SumCurrencyToRuble", "DateAddStock", "UserId") VALUES
(3, 'Австралийский доллар', 'AUD', 50, 200, 10000, '2025-10-23 16:20:24.828181+03', 1),
(4, 'Гонконгский доллар', 'HKD', 15, 200, 3000, '2025-10-24 10:37:03.268252+03', 1),
(5, 'Доллар США', 'USD', 77.75, 200, 15550.00, '2025-10-28 10:19:32.581527+03', 1),
(6, 'СДР (специальные права заимствования)', 'XDR', 50, 1, 50, '2025-11-06 15:02:43.859225+03', 1);
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
(32, 'mnty', NULL, 0.548, 555, 304.140, 24630.2912760, '2025-10-20 16:03:18.633585+03', 1),
(35, 'ALGOUSDT', 'weee', 50, 2, 100, 8118.8500, '2025-11-06 14:59:38.523644+03', 1);
INSERT INTO "public"."PlatformStartups" ("Id", "NamePlatform", "AmountCompanies", "SumOfStartups", "DateAddStock", "UserId") VALUES
(5, 'BrainBox', 2, 48100, '2025-11-12 15:34:26.550626+03', 1);
