# Unity Patrol Enemy AI | هوش مصنوعی دشمن گشت‌زن در یونیتی

## 🇮🇷 توضیحات فارسی

این پروژه شامل یک اسکریپت C# برای ساخت هوش مصنوعی دشمن در بازی‌های Unity است.

دشمن می‌تواند بین چند نقطه مشخص حرکت کند، بازیکن را شناسایی کرده و او را تعقیب کند. همچنین در صورت گم کردن بازیکن، می‌تواند مدتی منتظر بماند یا به رفتار گشت‌زنی خود برگردد.

### قابلیت‌ها
- حرکت بین نقاط مشخص (Patrol)
- شناسایی و تعقیب بازیکن (Chase)
- جست‌وجوی بازیکن پس از گم کردن او (Search)
- امکان تنظیم سرعت و نقاط حرکت دشمن
- قابلیت استفاده در پروژه‌های بازی‌سازی دوبعدی با Unity

### نحوه استفاده
1. فایل `PatrolEnemy.cs` را به پروژه Unity خود اضافه کنید.
2. اسکریپت را به آبجکت دشمن متصل کنید.
3. بازیکن و نقاط گشت‌زنی را در Inspector مشخص کنید.
4. تنظیمات لازم را انجام دهید و بازی را اجرا کنید.

**نکته:** قابلیت‌های دقیق این اسکریپت به پیاده‌سازی کد بستگی دارد.

---

## 🇬🇧 English Description

This project contains a C# script for creating enemy AI in Unity games.

The enemy can move between predefined patrol points, detect and chase the player, and search for the player after losing sight of them.

### Features
- Patrol between predefined checkpoints
- Detect and chase the player
- Search behavior after losing the player
- Configurable movement speed and patrol points
- Suitable for 2D Unity game development

### How to Use
1. Add `PatrolEnemy.cs` to your Unity project.
2. Attach the script to the enemy GameObject.
3. Assign the player and patrol checkpoints in the Inspector.
4. Configure the required settings and run the game.

**Note:** The actual features depend on the implementation of the script.

---

**Created by:** Sina Plex  
**Platform:** Unity  
**Language:** C#
