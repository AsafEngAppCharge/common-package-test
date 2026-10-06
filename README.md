# common-package-test

Minimal Unity **2022.3.55f1** project that proves a third UPM package can be shared by two SDKs.

## Layout

```
Packages/
  com.appcharge.common/     # shared dispatcher + GradleEnsure + ManifestEnsure
  com.appcharge.sdk.a/      # depends on com.appcharge.common 1.0.0
  com.appcharge.sdk.b/      # depends on com.appcharge.common 1.0.0
Assets/Plugins/Android/     # host templates both post-processors mutate
```

Embedded packages under `Packages/` are discovered automatically. `sdk.a` / `sdk.b` `package.json` declare:

```json
"dependencies": { "com.appcharge.common": "1.0.0" }
```

Unity resolves that from the sibling embedded package — **one** `Appcharge.Common.Threading` assembly, not two copies.

## What to verify in the Editor

1. Open this folder as a Unity project (2022.3.55f1).
2. Console should compile with no duplicate-type errors.
3. Menu **Appcharge → Common Test → Apply SDK A Android Ensures**, then the same for **SDK B**.
4. `Assets/Plugins/Android/mainTemplate.gradle` should contain:
   - `androidx.core:core-ktx:1.13.1` **once**
   - `com.appcharge:android-sdk-a:1.0.0`
   - `com.appcharge:android-sdk-b:1.0.0`
5. `AndroidManifest.xml` should contain `INTERNET` **once**, plus `REQUEST_INSTALL_PACKAGES` from B, plus two distinct engine meta keys.
6. Play mode: `CommonPackageSmokeTest` on a scene object logs two pings through the **same** dispatcher GameObject.

## What this is not

- Not a scoped registry. Git/file consumers still add `common` in the **project** manifest (or embed it) — a package `package.json` cannot pull another package from Git by URL.
- Product recipes stay in A/B. Common only has idempotent `Ensure*` helpers.
