DROP TABLE IF EXISTS "public"."__EFMigrationsHistory";
-- Table Definition
CREATE TABLE "public"."__EFMigrationsHistory" (
    "MigrationId" varchar(150) NOT NULL,
    "ProductVersion" varchar(32) NOT NULL,
    PRIMARY KEY ("MigrationId")
);


-- Indices
CREATE UNIQUE INDEX "PK___EFMigrationsHistory" ON public."__EFMigrationsHistory" USING btree ("MigrationId");

DROP TABLE IF EXISTS "public"."Startups";
-- Table Definition
CREATE TABLE "public"."Startups" (
    "Id" int4 NOT NULL,
    "NameCompany" varchar(20) NOT NULL,
    "NamePlatform" text NOT NULL,
    "Price" numeric NOT NULL,
    "AmountStock" int4 NOT NULL,
    "SumStocks" numeric NOT NULL,
    "DateAddStock" timestamptz NOT NULL,
    "UserId" int4 NOT NULL,
    "PlatformStartupId" int4 NOT NULL DEFAULT 0,
    CONSTRAINT "FK_Startups_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "public"."Users"("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Startups_PlatformStartups_PlatformStartupId" FOREIGN KEY ("PlatformStartupId") REFERENCES "public"."PlatformStartups"("Id") ON DELETE CASCADE,
    PRIMARY KEY ("Id")
);


-- Indices
CREATE UNIQUE INDEX "PK_Startups" ON public."Startups" USING btree ("Id");
CREATE INDEX "IX_Startups_UserId" ON public."Startups" USING btree ("UserId");
CREATE INDEX "IX_Startups_PlatformStartupId" ON public."Startups" USING btree ("PlatformStartupId");

DROP TABLE IF EXISTS "public"."Stocks";
-- Table Definition
CREATE TABLE "public"."Stocks" (
    "Id" int4 NOT NULL,
    "Ticker" varchar(4) NOT NULL,
    "NameCompany" text,
    "Price" numeric NOT NULL,
    "DateAddStock" timestamptz NOT NULL,
    "AmountStock" int4 NOT NULL DEFAULT 0,
    "SumStocks" numeric NOT NULL DEFAULT 0.0,
    "UserId" int4,
    CONSTRAINT "FK_Stocks_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "public"."Users"("Id"),
    PRIMARY KEY ("Id")
);


-- Indices
CREATE UNIQUE INDEX "PK_Stocks" ON public."Stocks" USING btree ("Id");

DROP TABLE IF EXISTS "public"."Metals";
-- Table Definition
CREATE TABLE "public"."Metals" (
    "Id" int4 NOT NULL,
    "NameMetal" text NOT NULL,
    "Price" numeric NOT NULL,
    "AmountMetal" numeric NOT NULL,
    "SumMetals" numeric NOT NULL DEFAULT 0.0,
    "DateAddStock" timestamptz NOT NULL,
    "UserId" int4 NOT NULL,
    CONSTRAINT "FK_Metals_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "public"."Users"("Id") ON DELETE CASCADE,
    PRIMARY KEY ("Id")
);


-- Indices
CREATE UNIQUE INDEX "PK_Metals" ON public."Metals" USING btree ("Id");
CREATE INDEX "IX_Metals_UserId" ON public."Metals" USING btree ("UserId");

DROP TABLE IF EXISTS "public"."RealEstates";
-- Table Definition
CREATE TABLE "public"."RealEstates" (
    "Id" int4 NOT NULL,
    "TypeEstate" text NOT NULL,
    "Price" numeric NOT NULL,
    "AmountEstate" int4 NOT NULL,
    "SumEstate" numeric NOT NULL,
    "DateAddStock" timestamptz NOT NULL,
    "UserId" int4 NOT NULL,
    "CityEstate" text NOT NULL DEFAULT ''::text,
    CONSTRAINT "FK_RealEstates_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "public"."Users"("Id") ON DELETE CASCADE,
    PRIMARY KEY ("Id")
);


-- Indices
CREATE UNIQUE INDEX "PK_RealEstates" ON public."RealEstates" USING btree ("Id");
CREATE INDEX "IX_RealEstates_UserId" ON public."RealEstates" USING btree ("UserId");

DROP TABLE IF EXISTS "public"."Users";
-- Table Definition
CREATE TABLE "public"."Users" (
    "Id" int4 NOT NULL,
    "Username" text NOT NULL,
    "PasswordHash" text NOT NULL,
    PRIMARY KEY ("Id")
);


-- Indices
CREATE UNIQUE INDEX "PK_Users" ON public."Users" USING btree ("Id");

DROP TABLE IF EXISTS "public"."StocksUSD";
-- Table Definition
CREATE TABLE "public"."StocksUSD" (
    "Id" int4 NOT NULL,
    "Ticker" varchar(4) NOT NULL,
    "NameCompany" text,
    "Price" numeric NOT NULL,
    "AmountStock" int4 NOT NULL,
    "SumStocks" numeric NOT NULL DEFAULT 0.0,
    "SumStocksToRuble" numeric NOT NULL DEFAULT 0.0,
    "DateAddStock" timestamptz NOT NULL,
    "UserId" int4 NOT NULL,
    CONSTRAINT "FK_StocksUSD_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "public"."Users"("Id") ON DELETE CASCADE,
    PRIMARY KEY ("Id")
);


-- Indices
CREATE INDEX "IX_StocksUSD_UserId" ON public."StocksUSD" USING btree ("UserId");
CREATE UNIQUE INDEX "PK_StocksUSD" ON public."StocksUSD" USING btree ("Id");

DROP TABLE IF EXISTS "public"."Currencies";
-- Table Definition
CREATE TABLE "public"."Currencies" (
    "Id" int4 NOT NULL,
    "NameCurrency" text NOT NULL,
    "CharCode" text NOT NULL,
    "Price" numeric NOT NULL,
    "AmountCurrency" numeric NOT NULL,
    "SumCurrencyToRuble" numeric NOT NULL,
    "DateAddStock" timestamptz NOT NULL,
    "UserId" int4 NOT NULL,
    CONSTRAINT "FK_Currencies_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "public"."Users"("Id") ON DELETE CASCADE,
    PRIMARY KEY ("Id")
);


-- Indices
CREATE UNIQUE INDEX "PK_Currencies" ON public."Currencies" USING btree ("Id");
CREATE INDEX "IX_Currencies_UserId" ON public."Currencies" USING btree ("UserId");

DROP TABLE IF EXISTS "public"."Cryptos";
-- Table Definition
CREATE TABLE "public"."Cryptos" (
    "Id" int4 NOT NULL,
    "Ticker" varchar(10) NOT NULL,
    "NameCrypto" text,
    "Price" numeric NOT NULL,
    "AmountCrypto" numeric NOT NULL,
    "SumCrypto" numeric NOT NULL DEFAULT 0.0,
    "SumCryptoToRuble" numeric NOT NULL DEFAULT 0.0,
    "DateAddStock" timestamptz NOT NULL,
    "UserId" int4 NOT NULL,
    CONSTRAINT "FK_Cryptos_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "public"."Users"("Id") ON DELETE CASCADE,
    PRIMARY KEY ("Id")
);


-- Indices
CREATE UNIQUE INDEX "PK_Cryptos" ON public."Cryptos" USING btree ("Id");
CREATE INDEX "IX_Cryptos_UserId" ON public."Cryptos" USING btree ("UserId");

DROP TABLE IF EXISTS "public"."PlatformStartups";
-- Table Definition
CREATE TABLE "public"."PlatformStartups" (
    "Id" int4 NOT NULL,
    "NamePlatform" text NOT NULL,
    "AmountCompanies" int4,
    "SumOfStartups" numeric,
    "DateAddStock" timestamptz NOT NULL,
    "UserId" int4 NOT NULL,
    CONSTRAINT "FK_PlatformStartups_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "public"."Users"("Id") ON DELETE CASCADE,
    PRIMARY KEY ("Id")
);


-- Indices
CREATE INDEX "IX_PlatformStartups_UserId" ON public."PlatformStartups" USING btree ("UserId");
CREATE UNIQUE INDEX "PK_PlatformStartups" ON public."PlatformStartups" USING btree ("Id");

INSERT INTO "public"."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES
('20250919085217_InitialCreate', '9.0.9'),
('20250919111954_InitialCreate', '9.0.9'),
('20250923102726_AddNewField', '9.0.9'),
('20250923121329_AddSumStock', '9.0.9'),
('20250923121505_AddSumStockk', '9.0.9'),
('20250923121811_AddSumStockks', '9.0.9'),
('20250923121944_DelReqNamecomp', '9.0.9'),
('20250923123245_Namecomp', '9.0.9'),
('20250923123611_Namecomp1', '9.0.9'),
('20250924070147_TickerChange', '9.0.9'),
('20250924083122_AddNewField1', '9.0.9'),
('20250924101224_AddNewField2', '9.0.9'),
('20250925045426_AuthService', '9.0.9'),
('20250925102811_Connectuser', '9.0.9'),
('20250925111126_Connectuser1', '9.0.9'),
('20250930085614_ModelCrypto', '9.0.9'),
('20250930121951_UpdateFieldCrypto', '9.0.9'),
('20251001110228_CryptoUser', '9.0.9'),
('20251001123124_Metalsss', '9.0.9'),
('20251002070231_SplitStocks', '9.0.9'),
('20251002101906_SplitStocks1', '9.0.9'),
('20251003050655_ddd', '9.0.9'),
('20251022115352_dfdf', '9.0.9'),
('20251023101418_newDBcurrency', '9.0.9'),
('20251023101816_newDBcurrency1', '9.0.9'),
('20251023113300_mnb', '9.0.9'),
('20251110073238_dfafsf', '9.0.9'),
('20251110073441_dfafsfл', '9.0.9'),
('20251110142147_newfieldinStartups', '9.0.9'),
('20251110161721_AddCascadeDeleteStartups', '9.0.9'),
('20251110161736_AddCascadeDeleteStartupss', '9.0.9'),
('20251112070402_UpdateField', '9.0.9'),
('20251117071655_NewDB', '9.0.9'),
('20251117071749_NewDBУыефеу', '9.0.9'),
('20251117071932_NewDBNewField', '9.0.9'),
('20251117100539_NewDBNewFieldasd', '9.0.9'),
('20251119103603_dfd', '9.0.9'),
('20251119103802_dfdл', '9.0.9'),
('20251119114224_dfdлd', '9.0.9');
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
