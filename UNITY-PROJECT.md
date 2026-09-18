# Unity project: current implemented state

This describes what is actually implemented in this repository right now —
a minimal Unity project bootstrap. It is not the pre-production planning pack
in `docs/` (a proposal), and it does not implement gameplay.

## Exact version

**Unity Editor 6000.3.2f1**, installed locally via Unity Hub. Use this exact
version; do not assume a different Unity 6 release behaves identically.

## Opening the project

1. Install Unity Hub and Unity Editor **6000.3.2f1** (matching version required).
2. Sign in to a Unity ID with an activated license (Personal or otherwise)
   before running the Editor — an unlicensed Editor will try to open Unity
   Hub's sign-in flow interactively.
3. Open Unity Hub → **Add** → select this repository's root folder
   (`/home/brian/projects/baby-farmer`, or your local clone path) → open with
   6000.3.2f1.
4. On first open, Unity imports assets and compiles scripts; there should be
   no console errors.

## Running the currently available tests

Both commands below were actually run against this repository and returned
exit code `0` with all tests passing. Adjust `-projectPath` for your local
clone.

```bash
# Domain-level EditMode smoke test (BabyFarmer.Domain.Tests)
Unity -batchmode -nographics \
  -projectPath /home/brian/projects/baby-farmer \
  -runTests -testPlatform EditMode \
  -testResults /tmp/editmode-results.xml \
  -logFile /tmp/editmode-run.log
```

```bash
# Presentation-layer PlayMode test: loads the minimal bootstrap scene and
# asserts it enters Play Mode with no unhandled console errors
# (BabyFarmer.Presentation.Tests)
Unity -batchmode -nographics \
  -projectPath /home/brian/projects/baby-farmer \
  -runTests -testPlatform PlayMode \
  -testResults /tmp/playmode-results.xml \
  -logFile /tmp/playmode-run.log
```

`Unity` above is the Editor binary, e.g.
`~/Unity/Hub/Editor/6000.3.2f1/Editor/Unity` on Linux. Both commands run
fully headless (`-batchmode -nographics`) and do not open any window.

You can also run tests interactively from **Window > General > Test Runner**
in the Editor (EditMode and PlayMode tabs).

## Project layout

```text
Assets/
  Scenes/
    Bootstrap.unity                     Minimal scene (Main Camera, Directional Light, one marker object)
  Scripts/
    Domain/          BabyFarmer.Domain            Game-rule assembly; no UnityEngine/UnityEditor reference
                                                    ("noEngineReferences": true in its .asmdef — enforced at compile time)
    Presentation/     BabyFarmer.Presentation      Unity-facing runtime assembly; may reference Domain and Unity APIs
    Tests/Domain/      BabyFarmer.Domain.Tests      EditMode tests for the domain assembly
    Tests/Presentation/ BabyFarmer.Presentation.Tests PlayMode test that loads Bootstrap.unity
Packages/manifest.json  Only the platform SDK/toolchain modules Unity's project template requires,
                         plus com.unity.test-framework. No speculative/feature packages
                         (e.g. Multiplayer Center) were kept.
```

## Source-control-friendly settings

Verified in the generated project settings — both already correct by default
for a new Unity 6000.3.2f1 project, so no change was required:

- `ProjectSettings/EditorSettings.asset`: `m_SerializationMode: 2` (Force Text).
- `ProjectSettings/VersionControlSettings.asset`: `m_Mode: Visible Meta Files`.

`.meta` files are in fact generated alongside every asset as plain text; this
was confirmed by inspecting the actual generated `.meta` files (e.g.
`Assets/Scenes/Bootstrap.unity.meta`), not just by reading the setting.

## Known limitations

- Command-line validation above uses `-nographics`, so it does not exercise
  actual rendering. Visual/rendering correctness in a real windowed session
  still needs a human to open the project and confirm it — this repository
  only claims what the headless commands above actually proved.
- No build/player pipeline is set up yet; this issue only bootstraps the
  Editor-side project.
