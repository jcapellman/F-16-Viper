# .NET 10 Upgrade Plan

## Table of Contents

- [Executive Summary](#executive-summary)
- [Migration Strategy](#migration-strategy)
- [Detailed Dependency Analysis](#detailed-dependency-analysis)
- [Project-by-Project Plans](#project-by-project-plans)
- [Package Update Reference](#package-update-reference)
- [Breaking Changes Catalog](#breaking-changes-catalog)
- [Testing and Validation Strategy](#testing-and-validation-strategy)
- [Risk Management](#risk-management)
- [Complexity and Effort Assessment](#complexity-and-effort-assessment)
- [Source Control Strategy](#source-control-strategy)
- [Success Criteria](#success-criteria)

## Executive Summary

### Selected Strategy
**All-At-Once Strategy** — upgrade all projects in the solution simultaneously in a single coordinated operation.

### Scope
This solution contains 3 projects:

- `F16Viper.Common.library\F16Viper.Common.library.csproj` — currently `netstandard2.0`
- `F16Viper.LevelEditor\F16Viper.LevelEditor.csproj` — currently `netcoreapp3.0`
- `F16Viper.library\F16Viper.library.shproj` — currently `netcore4.5.1`

### Target State

- `F16Viper.Common.library` remains on `netstandard2.0`
- `F16Viper.LevelEditor` moves to `net10.0-windows`
- `F16Viper.library` moves to `net10.0`

### Why This Strategy Fits

- The solution is small enough for an atomic upgrade.
- Assessment shows no NuGet package incompatibilities.
- The main risk is concentrated in the WPF application, not in the dependency graph.
- The shared library already targets `netstandard2.0`, so it remains compatible with the upgraded application.

### Current Risk Picture

- `F16Viper.LevelEditor` carries the highest risk because of 58 binary-incompatible APIs and 5 behavioral changes.
- WPF APIs are the dominant migration concern.
- No package security issues were reported.
- No cycles or complex project graph issues were found.

### Recommended Outcome

Perform one coordinated framework upgrade across the solution, then build and test the full solution until it is stable on .NET 10.

## Migration Strategy

### Approach
Use a single atomic upgrade pass for all projects rather than a staged migration.

### Rationale

- There are only 3 projects in the solution.
- Dependency structure is simple: `F16Viper.LevelEditor` depends on `F16Viper.Common.library`.
- `F16Viper.Common.library` does not require framework changes, but it must stay aligned with the upgraded consumer.
- The application is WPF-based, so framework-related changes are best handled in one coordinated update.

### Ordering Within the Atomic Change

Even though the upgrade is atomic, the implementation should respect dependency order during review and validation:

1. Confirm shared library compatibility.
2. Update application and supporting project targets together.
3. Validate WPF-specific changes in the application.
4. Restore, build, and test the entire solution.

### Notes on Target Frameworks

- `F16Viper.LevelEditor` should target `net10.0-windows` because it is a WPF application.
- `F16Viper.library` should target `net10.0`.
- `F16Viper.Common.library` should remain `netstandard2.0` unless a later assessment identifies a reason to raise it.

## Detailed Dependency Analysis

### Dependency Graph Summary

- `F16Viper.Common.library` is the leaf/shared dependency.
- `F16Viper.LevelEditor` is the primary application and consumes the shared library.
- `F16Viper.library` is independent in the assessment graph.

### Migration Grouping

Because this is an all-at-once upgrade, all projects are treated as one migration unit:

- Shared library: `F16Viper.Common.library`
- WPF application: `F16Viper.LevelEditor`
- Supporting project: `F16Viper.library`

### Critical Path

1. Update project target frameworks.
2. Resolve any WPF framework incompatibilities in `F16Viper.LevelEditor`.
3. Verify the shared library still compiles cleanly.
4. Validate the solution as a whole.

### Special Considerations

- WPF upgrade work should be reviewed carefully because the assessment reported numerous binary-incompatible APIs.
- `System.Uri` behavioral changes should be checked during testing.
- No circular dependencies were reported.

## Project-by-Project Plans

### `F16Viper.Common.library\F16Viper.Common.library.csproj`

**Current State**

- Target framework: `netstandard2.0`
- Project type: class library
- Dependencies: none
- Dependants: `F16Viper.LevelEditor`
- Risk level: Low

**Target State**

- Keep targeting `netstandard2.0`
- Preserve compatibility with the upgraded WPF application

**Migration Steps**

1. Confirm no framework change is required.
2. Preserve existing package references unless later validation reveals a need.
3. Rebuild in the context of the upgraded solution to verify compatibility.

**Expected Impact**

- No API incompatibilities were reported for this project.
- No package updates are required from the assessment.

**Validation Checklist**

- Builds successfully in the upgraded solution
- No dependency resolution issues
- No new warnings or compatibility regressions introduced by the application upgrade

### `F16Viper.LevelEditor\F16Viper.LevelEditor.csproj`

**Current State**

- Target framework: `netcoreapp3.0`
- Proposed target framework: `net10.0-windows`
- Project type: WPF desktop application
- Dependencies: `F16Viper.Common.library`
- Risk level: High

**Target State**

- Run as a .NET 10 WPF desktop application
- Preserve the existing app structure while updating framework-specific references and WPF usage patterns

**Migration Steps**

1. Update the project target framework to `net10.0-windows`.
2. Ensure Windows desktop support is explicitly enabled where required.
3. Review WPF-related code for framework compatibility.
4. Address `System.Uri` behavioral changes and other API-level differences identified in the assessment.
5. Reconcile any XAML, startup, window, binding, or event handler code affected by WPF framework changes.
6. Validate the upgraded app within the full solution.

**Expected Breaking-Change Focus Areas**

- `System.Windows.*` WPF APIs
- Event handler signatures and routed event behavior
- `Application`, `Window`, `ListView`, `ContextMenu`, and related control APIs
- `System.Uri` behavior changes

**Validation Checklist**

- Project builds on `net10.0-windows`
- WPF startup and window loading work as expected
- Shared library references resolve cleanly
- All identified compatibility issues are addressed or consciously accepted

### `F16Viper.library\F16Viper.library.shproj`

**Current State**

- Target framework: `netcore4.5.1`
- Proposed target framework: `net10.0`
- Project type: .NET application/supporting project
- Dependencies: none
- Risk level: Low

**Target State**

- Move to `net10.0`
- Keep the project aligned with the upgraded solution baseline

**Migration Steps**

1. Update the project target framework to `net10.0`.
2. Verify any project-specific SDK or import settings remain valid.
3. Confirm the project compiles and participates correctly in the solution after the framework update.

**Validation Checklist**

- Framework target updated successfully
- No project system or SDK import issues remain
- Solution build succeeds with this project included

## Package Update Reference

### Summary

The assessment found 2 NuGet packages, both already compatible. No package upgrades are required for this migration.

### Package Matrix

| Package | Current Version | Target Version | Projects Affected | Reason |
|---|---:|---:|---|---|
| `NETStandard.Library` | `2.0.3` | No change | `F16Viper.Common.library` | Already compatible |
| `Newtonsoft.Json` | `13.0.4` | No change | `F16Viper.Common.library` | Already compatible |

## Breaking Changes Catalog

### High-Priority Areas

#### WPF API compatibility

The assessment reported many binary-incompatible WPF APIs. Focus areas include:

- `System.Windows.Application`
- `System.Windows.Window`
- `System.Windows.Controls.ListView`
- `System.Windows.Controls.MenuItem`
- `System.Windows.Controls.ContextMenu`
- `System.Windows.Controls.ItemCollection`
- Routed event handlers and related event patterns

#### Behavioral changes

- `System.Uri`
- `System.Uri` constructors

### Expected Work During Validation

- Replace or adapt APIs whose signatures or availability changed.
- Confirm WPF app startup and control behavior remain correct.
- Watch for subtle behavior changes in URI handling and UI interactions.

## Testing and Validation Strategy

### Validation Sequence

1. Restore dependencies for the upgraded solution.
2. Build the full solution.
3. Resolve all framework-related compilation issues.
4. Rebuild to confirm the solution is stable.
5. Run available test projects.

### What to Validate

- Solution compiles without errors
- WPF application launches and loads its main window
- Shared library remains compatible with the upgraded application
- No regressions in URI handling or WPF control behavior

### Test Scope

- Unit tests, if present in the solution
- Any integration or UI automation tests, if available

## Risk Management

### Risk Table

| Project | Risk Level | Main Risk | Mitigation |
|---|---|---|---|
| `F16Viper.LevelEditor` | High | WPF binary incompatibilities and behavioral changes | Review WPF APIs carefully, validate startup and UI behavior, and resolve issues in the same coordinated upgrade |
| `F16Viper.Common.library` | Low | Downstream compatibility with upgraded app | Keep target framework stable and validate against the upgraded consumer |
| `F16Viper.library` | Low | Project system or framework alignment issues | Confirm project settings remain valid after the target framework update |

### Contingency Plan

- If WPF changes cause build failures, focus remediation on the application project first.
- If the shared library introduces compatibility issues unexpectedly, hold the framework target steady and validate references before broadening the change.
- If runtime behavior differs, prioritize the high-frequency WPF and URI issues listed in the assessment.

## Complexity and Effort Assessment

### Project Complexity

| Project | Complexity | Reason |
|---|---|---|
| `F16Viper.Common.library` | Low | Small library with no compatibility issues |
| `F16Viper.LevelEditor` | High | WPF app with many binary-incompatible APIs and behavioral changes |
| `F16Viper.library` | Low | No dependencies and no API issues reported |

### Overall Solution Complexity

- Solution size is small.
- Risk is concentrated in one WPF project.
- Dependency graph is simple.
- Overall migration complexity is moderate despite the high-risk application project.

## Source Control Strategy

- Branch for upgrade work: `upgrade-to-NET10`
- Keep the upgrade contained to a single branch.
- Prefer a single coherent commit for the final upgrade state if repository policy allows it.
- If intermediate checkpoints are necessary, keep them limited to the upgrade branch only.

## Success Criteria

The migration is complete when all of the following are true:

1. All projects target their planned .NET 10-compatible frameworks.
2. The solution restores successfully.
3. The solution builds successfully.
4. Available tests pass.
5. The WPF application runs correctly on the new framework.
6. No package incompatibilities remain.
7. No unresolved framework-related issues remain from the assessment.
