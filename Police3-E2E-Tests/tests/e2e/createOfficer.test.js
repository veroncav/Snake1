/**
 * Исходное состояние: Админ авторизован.
 * Действие: Добавляет нового офицера через UI.
 * Ожидаемый результат: Новый офицер появляется в списке и затем удаляется.
 */
const { test, expect } = require('@playwright/test');
require('dotenv').config();

test('Admin creates and then deletes new officer', async ({ page }) => {
  await page.goto(process.env.BASE_URL + '/login2.php');
  await page.fill('input[name="kasutaja"]', process.env.ADMIN_USER);
    await page.fill('input[name="parool""]', process.env.ADMIN_PASS);
  await page.click('button[type="submit"]');
  await page.goto(process.env.BASE_URL + '/officers.php');

  // click "Add" (try several common selectors)
  const addButtons = ['text=Add Officer', 'text=Add', 'button#addOfficer', 'button:has-text("Add")'];
  let addFound = null;
  for (const sel of addButtons) {
    if (await page.locator(sel).count() > 0) { addFound = sel; break; }
  }
  if (!addFound) throw new Error('Add Officer button not found - please adjust selector');

  await page.click(addFound);
  const testName = 'Test_Officer_' + Date.now();
  // Try common name attributes - adjust if necessary
  const firstNameField = ['input[name="first_name"]','input[name="firstname"]','input[name="fname"]','input[placeholder="First name"]'];
  for (const f of firstNameField) {
    if (await page.locator(f).count() > 0) { await page.fill(f, testName); break; }
  }
  // attempt other fields with common names
  const lastField = ['input[name="last_name"]','input[name="lastname"]','input[name="lname"]'];
  for (const f of lastField) { if (await page.locator(f).count() > 0) { await page.fill(f, 'Auto'); break; } }
  const rankField = ['input[name="rank"]','input[name="position"]'];
  for (const f of rankField) { if (await page.locator(f).count() > 0) { await page.fill(f, 'Tester'); break; } }
  const idField = ['input[name="id_code"]','input[name="idcode"]','input[name="id"]'];
  for (const f of idField) { if (await page.locator(f).count() > 0) { await page.fill(f, '90000000000'); break; } }

  // submit - try common selectors
  const submitButtons = ['button[type="submit"]','text=Save','text=Create','button:has-text("Save")'];
  let submitFound = null;
  for (const s of submitButtons) { if (await page.locator(s).count() > 0) { submitFound = s; break; } }
  if (!submitFound) throw new Error('Submit button not found - please adjust selector');
  await page.click(submitFound);

  // verify new officer present
  await expect(page.locator(`text=${testName}`)).toBeVisible();

  // then delete the created officer to keep test isolated
  // try to find a delete button in the same row
  const row = page.locator(`tr:has-text("${testName}")`);
  await expect(row).toBeVisible();
  const deleteButtons = ['button.deleteOfficer','text=Delete','button:has-text("Delete")'];
  for (const d of deleteButtons) {
    if (await row.locator(d).count() > 0) { await row.locator(d).first().click(); break; }
  }
  // assert it's gone
  await expect(page.locator(`text=${testName}`)).not.toBeVisible();
});
