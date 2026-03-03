# AI Agents Guidelines

## Development Rules

### 1. Build & Test Verification
Before committing any changes:
- Run `dotnet build --configuration Release` to ensure build succeeds
- Run `dotnet test --configuration Release` to ensure all tests pass
- Fix any build failures or test failures immediately

### 2. Code Quality Standards
- **No compiler warnings**: The project has `TreatWarningsAsErrors=true` enabled
- Do not generate code that produces compiler warnings
- All changes must compile cleanly in Release configuration

### 3. Branching Strategy
- **For new features**: Always create a new branch (e.g., `feature/feature-name`)
- **For bug fixes**: Create a dedicated branch (e.g., `fix/bug-name`)
- **Never commit directly to `main`** - changes must go through pull requests
- Merge to main only after CI/CD passes and code review is approved

### 4. Commit Guidelines
- Include descriptive commit messages
- Add the Copilot co-author trailer:
  ```
  Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>
  ```
- One feature/fix per commit when possible

### 5. CI/CD Pipeline Checks
Your changes will be validated by:
- **Build Check**: Must compile without warnings or errors
- **Test Check**: All unit tests must pass
- **CodeQL**: Security vulnerability scanning
- Merge is blocked if any check fails
