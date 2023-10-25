# Binding sources

## IObservable
  - is an INotifyProertyChanged and INotifyPropertyChanging
  - as such, it raises PropertyChanged and PropertyChanging events whenever any of its properties change.

## ObservableObject
  - is an implementation of IObservable
  - when any of its properties change, it raises the event with the name of the property
  - in order for this to work, deriving classes MUST utilize the provided Set methods in their property setters, rather than setting the properties directly.  This will ensure the events are raised properly
  - an example of ObservableObject is any of the simple classes in the Openworld.Models namespace

## ObservableArray
  - is a generic ObservableObject that provides a single observable property "Items"
  - perhaps in the future this could provide methods and property change events that allow updating specific items within the array, but for now it's just the array itself 

## ObservableMonoBehaviour
  - a Unity MonoBehaviour that is observable
  - it is used to ensure that the deriving class implements the IObservable methods
  - examples include GameManager, CharacterOverviewPanel, CharacterCreator, and CharacterSkillContainer

## IBindingProvider
  - an IObservable that provides a binding source for components that wish to bind to an ObservableObject
  - Each binding provider can only provide a single property as a binding source
  - the binding source is an ObservableObject that raises events whenever any of its properties change
  - the binding source needs to be observable so that if the source changes, subscribers can update their references
  - examples include CharacterSkillContainer and CharacterOverviewPanel
  - The CharacterSkillContainer provides an ObservableArray of Skills for binding to the SkillSelector dropdown
  - The CharacterOverviewPanel provides the Character object for binding character properties to display components

# Binding observers

## BoundComponent
  - a MonoBehaviour with methods to find an IBindingProvider
  - binds a property on an ObservableObject to a target, usually a property of this component
  - has handlers for binding source change AND binding source property change
  - these handlers are responsible for updating the target property
  - by default the bound component only observes a single property on the binding source
  - examples: BoundStatusBar, BoundTextComponent
  - BoundStatusBar binds a status bar to an integer value, such as hitpoints, mana, stamina, etc
  - BoundTextComponent binds a TMP_Text component to a string value

## CharacterStatDisplay
  - a complex example of a components composed of multiple bound components.
  - the CharacterStatDisplay is responsible for setting the binding properties on its sub-components
