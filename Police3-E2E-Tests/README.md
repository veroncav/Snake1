# Police3 E2E Tests (ready-to-run)

Этот архив содержит готовый проект с E2E тестами для сайта Police3.
Цель: выдать готовый набор, который можно открыть в VS Code и запустить командой `npm run test:e2e`.

## Системные требования
- OS: Windows / macOS / Linux
- Node.js >= 20
- Рекомендуем: Visual Studio Code
- Браузеры: Chromium/Firefox/WebKit (устанавливаются Playwright)

## Как запустить (быстро)
1. Распакуйте архив и откройте папку `Police3-E2E-Tests` в VS Code.
2. Откройте терминал (Ctrl+`).
3. Установите зависимости:
   ```bash
   npm install
   npx playwright install
   ```
4. Создайте файл с переменными окружения:
   ```bash
   cp .env.example .env
   ```
   (если вы на Windows PowerShell: `Copy-Item .env.example .env`)
5. Запустите тесты:
   ```bash
   npm run test:e2e
   ```
6. Просмотр отчёта:
   ```bash
   npm run show-report
   ```

## Что внутри
- 5 независимых E2E тестов в `tests/e2e/`
- `.env.example` — пример переменных окружения (без паролей в коде)
- `playwright.config.js` — конфигурация Playwright
- `REPORT.md` — краткий отчёт-шаблон

## Примечания
- Тесты работают с сайтом: https://veronikavanitseva24.thkit.ee/Police3
- Логины: Admin / 12345, kasutaja / 12345 (указано в .env.example)
- Тесты стараются создавать уникальные тестовые записи (добавляют timestamp) и удалять их в конце, чтобы не «протекали».
