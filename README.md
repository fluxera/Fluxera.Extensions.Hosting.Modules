# Fluxera.Extensions.Hosting.Modules

Einleitung...


## Concepts

- Modules
  - Is the logical unit of one or more features that are enabled by using it as dependency.
- Contributors
  - Are components that provide additional startup logic to a module that provides a sertain feature.
- Extensions
  - Are similar to contributors but are used at runtime. Extensions are always additive so that not only
    one instace is used, but all registered extensions. Extensions are fixed it the sense that the module
    it belongs to will always configure it to be used.
- Plugins
  - A plugin implements interfaces provided by one or more modules and is used using the AddPlugin
    infrastructure. It could be used as normal module but is usually used this way because different
    implementations are usd because of the configuration of the application.

## Configuration

appsettings.json ...

```json
{
  "Hosting": {
    "Modules": {
      "DataManagement": {
      }
    }
  },
  "ConnectionStrings": {
    "Default": "localhost"
  }
}
```