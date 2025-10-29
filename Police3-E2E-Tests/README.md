# Police3 E2E Tests

## Overview

This project contains automated end-to-end (E2E) tests for the **Police3** web application.  
The tests verify that the application’s main functionalities for both admin and regular users work correctly.

---

## Tested features

- **Admin login**  
  - Administrator can log in successfully.

- **Regular user login**  
  - Regular users can log in.  
  - Admin-only features are hidden for regular users.

- **User role management**  
  - Admin can assign or remove the admin role from other users.  
  - The current UI does not provide a "create new user" form; tests verify that this control is not present.

- **Editing user role**  
  - Admin can change a user role (Admin ↔ Regular user).

- **Deleting roles/users**  
  - Admin can remove the admin role from a user (role removal is covered by tests).

---

## Test accounts

Use these test credentials when reviewing the tests or running them:

- **Admin**
  - Username: `Admin`
  - Password: `12345`

- **Regular user**
  - Username: `kasutaja`
  - Password: `12345`

These credentials are for testing the deployed site at `http://veronikavanitseva24.thkit.ee/Police3`



