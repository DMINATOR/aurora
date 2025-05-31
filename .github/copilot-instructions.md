---
applyTo: "**"
---
# Project general coding standards

## Naming Conventions
- Use PascalCase for class names, interfaces, type aliases, and public properties
- Use camelCase for variables, functions, methods, and private/protected properties
- Prefix private class members with underscore (_)
- Use ALL_CAPS for constants and static readonly fields
- Place each class, interface, or enum in its own file
- Group related files in folders by feature or domain

## Error Handling
- Use try/catch blocks for async and sync operations where exceptions may occur
- Always log errors with contextual information, including exception details
- Prefer logging via delegates (e.g., OutputSink, ErrorSink) or a logging framework

## Documentation
- Use XML documentation comments (///) for all public classes, methods, and properties
- Add summary and parameter descriptions where appropriate

## Test Code
- Use [Fact] and [Theory] attributes for xUnit tests
- Name test methods descriptively (e.g., MethodName_Condition_ExpectedResult)
- Log test output and errors using the provided test output helper

## File & Folder Structure
- Organize code into folders by feature or domain (e.g., AI, Constants, Models)
- Keep related types together, but use one type per file

## General
- Prefer explicit access modifiers (public, private, protected, internal)
- Avoid magic strings and numbers; use constants or enums
- Use using directives at the top of files, sorted alphabetically
- Keep code clean and readable with consistent indentation and spacing