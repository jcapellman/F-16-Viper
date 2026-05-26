# F-16 Viper .NET 10 Upgrade Tasks

## Overview

This document tracks the execution of the F-16 Viper solution upgrade from .NET Core 3.0 and netcore4.5.1 to .NET 10. All projects will be upgraded simultaneously in a single atomic operation.

**Progress**: 1/2 tasks complete (50%) ![0%](https://progress-bar.xyz/50)

---

## Tasks

### [✓] TASK-001: Atomic framework upgrade with compilation fixes *(Completed: 2026-05-26 00:29)*
**References**: Plan §Project-by-Project Plans, Plan §Breaking Changes Catalog

- [✓] (1) Update `F16Viper.LevelEditor\F16Viper.LevelEditor.csproj` target framework from `netcoreapp3.0` to `net10.0-windows`
- [✓] (2) Update `F16Viper.library\F16Viper.library.shproj` target framework from `netcore4.5.1` to `net10.0`
- [✓] (3) Confirm `F16Viper.Common.library\F16Viper.Common.library.csproj` remains on `netstandard2.0` (no changes needed)
- [✓] (4) All project target frameworks updated correctly (**Verify**)
- [✓] (5) Restore dependencies for the solution
- [✓] (6) Dependencies restored successfully (**Verify**)
- [✓] (7) Build solution and fix all WPF API compatibility issues per Plan §Breaking Changes Catalog (focus areas: System.Windows.Application, System.Windows.Window, System.Windows.Controls classes, routed event handlers, System.Uri behavioral changes)
- [✓] (8) Solution builds with 0 errors (**Verify**)

---

### [ ] TASK-002: Final commit
**References**: Plan §Source Control Strategy

- [ ] (1) Commit all changes with message: "TASK-002: Complete upgrade to .NET 10"

---
