# Contributing to Network Setter

Thank you for considering contributing to Network Setter! This document provides guidelines for contributing to the project.

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK
- Windows 10/11
- Visual Studio 2022, VS Code, or JetBrains Rider (optional)
- Git

### Setting Up Development Environment

1. **Fork the repository**
   ```bash
   # Click the "Fork" button on GitHub
   ```

2. **Clone your fork**
   ```bash
   git clone https://github.com/RUSHYOP/network-setter.git
   cd network-setter
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```
   > Remember to run as Administrator

## 🔧 Development Guidelines

### Code Style

- Follow standard C# naming conventions
- Use meaningful variable and method names
- Add XML documentation comments for public methods
- Keep methods focused and concise
- Use async/await for I/O operations where appropriate

### Example

```csharp
/// <summary>
/// Applies network configuration to the specified adapter.
/// </summary>
/// <param name="config">Network configuration to apply</param>
/// <exception cref="Exception">Thrown when configuration fails</exception>
public static void ApplyConfig(NetworkConfig config)
{
    // Implementation
}
```

### Commit Messages

Use clear and descriptive commit messages:

- ✅ Good: `Add IPv6 support for network configuration`
- ✅ Good: `Fix: Resolve adapter refresh issue on Windows 11`
- ✅ Good: `Docs: Update README with installation instructions`
- ❌ Bad: `fix bug`
- ❌ Bad: `update`

#### Commit Message Format

```
<type>: <subject>

<body (optional)>

<footer (optional)>
```

**Types:**
- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation changes
- `style`: Code style changes (formatting, etc.)
- `refactor`: Code refactoring
- `test`: Adding or updating tests
- `chore`: Maintenance tasks

## 🐛 Reporting Bugs

### Before Submitting a Bug Report

1. Check existing issues to avoid duplicates
2. Verify the bug exists in the latest version
3. Collect relevant information:
   - Windows version
   - .NET version
   - Steps to reproduce
   - Expected vs actual behavior
   - Error messages or logs

### Bug Report Template

```markdown
**Description**
A clear description of the bug.

**Steps to Reproduce**
1. Go to '...'
2. Click on '...'
3. See error

**Expected Behavior**
What you expected to happen.

**Actual Behavior**
What actually happened.

**Environment**
- OS: Windows 11 22H2
- .NET: 8.0.3
- App Version: 1.0.0

**Screenshots**
If applicable, add screenshots.

**Additional Context**
Any other relevant information.
```

## 💡 Suggesting Features

We welcome feature suggestions! Please:

1. Check if the feature has already been suggested
2. Clearly describe the feature and its benefits
3. Provide use cases
4. Consider implementation complexity

### Feature Request Template

```markdown
**Feature Description**
Clear description of the proposed feature.

**Use Case**
Why is this feature needed? What problem does it solve?

**Proposed Solution**
How do you envision this feature working?

**Alternatives Considered**
What alternative solutions have you considered?

**Additional Context**
Mockups, examples, or references.
```

## 🔀 Pull Request Process

### Before Submitting

1. **Create a feature branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Make your changes**
   - Write clean, well-documented code
   - Follow the project's code style
   - Test your changes thoroughly

3. **Commit your changes**
   ```bash
   git add .
   git commit -m "feat: Add your feature description"
   ```

4. **Push to your fork**
   ```bash
   git push origin feature/your-feature-name
   ```

### Submitting the Pull Request

1. Go to your fork on GitHub
2. Click "New Pull Request"
3. Fill out the PR template:

```markdown
## Description
Brief description of changes.

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Testing
How has this been tested?

## Checklist
- [ ] Code follows project style guidelines
- [ ] Self-reviewed the code
- [ ] Commented code where necessary
- [ ] Documentation updated
- [ ] No new warnings generated
- [ ] Tested on Windows 10/11
```

### Review Process

1. A maintainer will review your PR
2. Address any requested changes
3. Once approved, your PR will be merged
4. Your contribution will be acknowledged!

## 🧪 Testing

### Manual Testing Checklist

Before submitting changes, test:

- [ ] Application starts without errors
- [ ] Network adapters load correctly
- [ ] Current settings display accurately
- [ ] Static IP configuration works
- [ ] DHCP configuration works
- [ ] IPv4 settings apply correctly
- [ ] IPv6 settings apply correctly (if applicable)
- [ ] Presets save successfully
- [ ] Presets load and apply correctly
- [ ] Presets delete without errors
- [ ] UI is responsive and clear
- [ ] Error messages are helpful
- [ ] Admin elevation works correctly

### Test Network Configurations

Test with various scenarios:
- Different network adapters (Wi-Fi, Ethernet)
- Valid and invalid IP addresses
- Empty and partial configurations
- Very long preset names
- Special characters in inputs

## 📝 Documentation

When adding features:

1. Update relevant `.md` files
2. Add code comments for complex logic
3. Update `README.md` if user-facing
4. Add examples where helpful

## 🏗️ Project Structure

Understanding the codebase:

```
network-setter/
├── MainForm.cs              # UI logic and event handlers
├── NetworkManager.cs        # Core network operations (netsh, WMI)
├── NetworkConfig.cs         # Data model for configurations
├── PresetManager.cs         # Preset persistence (JSON)
├── Program.cs               # Application entry point
└── app.manifest             # UAC elevation configuration
```

### Key Components

**MainForm.cs**
- User interface
- Event handlers
- Data binding

**NetworkManager.cs**
- Network adapter enumeration
- Configuration reading (WMI)
- Configuration writing (netsh)

**PresetManager.cs**
- JSON serialization
- File I/O
- Preset CRUD operations

## 🤔 Questions?

If you have questions:

1. Check the documentation
2. Search existing issues
3. Ask in a new issue with the `question` label
4. Be specific and provide context

## 📜 Code of Conduct

### Our Standards

- Be respectful and inclusive
- Welcome newcomers
- Accept constructive criticism
- Focus on what's best for the project

### Unacceptable Behavior

- Harassment or discriminatory language
- Personal attacks
- Spam or trolling
- Publishing others' private information

## 🎉 Recognition

Contributors will be:
- Listed in the project's contributors
- Credited in release notes
- Acknowledged in the README (for significant contributions)

## 📧 Contact

For sensitive matters, contact the maintainers directly.

---

Thank you for contributing to Network Setter! 🚀
