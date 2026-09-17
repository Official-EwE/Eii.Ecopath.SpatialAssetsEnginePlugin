# Copilot Instructions — TemplateRepoWindowsEii
This is a template instructions file. 
Change this file to provide instructions specific to your project.

## Unit tests
- Test project: `TemplateRepoWindowsEii.Tests` (xUnit, net10.0).
- Use pinned versions for test libraries:
  - `Moq` version `4.18.0`
  - `FluentAssertions` version `6.6.0`
- **All tests must follow the Arrange-Act-Assert (AAA) pattern, with explicit `// Arrange`, `// Act`, and `// Assert` comments marking each section.** Use `// Act & Assert` for a combined section (e.g., `Assert.ThrowsAsync`).
- Name tests `MethodUnderTest_ExpectedBehaviour` (e.g., `UpdateCatchDisposition_AccumulatesAcrossMonthlyCalls`).
- Use `Moq` to mock interfaces (e.g., `IMSEStockRecruitment`, `IMSEQuotaCalculator`) and `FluentAssertions` for assertions (`.Should().Be(...)`, `.Should().ThrowAsync<...>()`).
- Use shared private helpers to build test data (e.g., `CreateInitialisedServiceAsync`, `CreateGrid`).
CancellationToken`.
