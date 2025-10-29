/**
 * Исходное состояние: Админ авторизован.
 * Действие: Создаёт временного офицера, редактирует его и удаляет.
 * Ожидаемый результат: Изменение отображается, затем тестовые данные удаляются.
 */
const { test, expect } = require('@playwright/test');
require('dotenv').config();

test('Admin creates, edits and deletes officer', async ({ page }) => {
  await page.goto(process.env.BASE_URL + '/login2.php');
  await page.fill('input[name="kasutaja"]', process.env.ADMIN_USER);
    await page.fill('input[name="parool""]', process.env.ADMIN_PASS);
  await page.click('button[type="submit"]');
  await page.goto(process.env.BASE_URL + '/officers.php');

  // create
  const addSel = ['text=Add Officer','text=Add','button#addOfficer','button:has-text("Add")'];
  let addFound = null;
  for (const s of addSel) { if (await page.locator(s).count()>0) { addFound=s; break; } }
  if (!addFound) throw new Error('Add button not found');
  await page.click(addFound);

  const testName = 'Edit_Officer_' + Date.now();
  const firstNameField = ['input[name="first_name"]','input[name="firstname"]','input[name="fname"]'];
  for (const f of firstNameField) { if (await page.locator(f).count()>0) { await page.fill(f,testName); break; } }
  const lastField = ['input[name="last_name"]','input[name="lastname"]'];
  for (const f of lastField) { if (await page.locator(f).count()>0) { await page.fill(f,'Tmp'); break; } }
  const submitButtons = ['button[type="submit"]','text=Save','text=Create','button:has-text("Save")'];
  for (const s of submitButtons) { if (await page.locator(s).count()>0) { await page.click(s); break; } }

  // find created row and click Edit
  const row = page.locator(`tr:has-text("${testName}")`);
  await expect(row).toBeVisible();
  const editButtons = ['text=Edit','button.editOfficer','button:has-text("Edit")'];
  for (const e of editButtons) { if (await row.locator(e).count()>0) { await row.locator(e).first().click(); break; } }

  // change rank field
  const rankField = ['input[name="rank"]','input[name="position"]'];
  const newRank = 'EditedRank_' + Date.now();
  let rankFound=false;
  for (const f of rankField) {
    if (await page.locator(f).count()>0) { await page.fill(f, newRank); rankFound=true; break; }
  }
  if (!rankFound) {
    // try an editable cell approach
    await page.click(`tr:has-text("${testName}") td:has-text("Rank")`);
  }
  // save
  const saveButtons = ['button[type="submit"]','text=Save','button:has-text("Save")'];
  for (const s of saveButtons) { if (await page.locator(s).count()>0) { await page.click(s); break; } }

  // verify new rank visible
  await expect(page.locator(`text=${newRank}`)).toBeVisible();

  // cleanup - delete the row
  const deleteButtons = ['button.deleteOfficer','text=Delete','button:has-text("Delete")'];
  for (const d of deleteButtons) {
    if (await row.locator(d).count()>0) { await row.locator(d).first().click(); break; }
  }
  await expect(page.locator(`text=${testName}`)).not.toBeVisible();
});
