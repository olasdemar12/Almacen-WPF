# Dependency Properties
Una Dependency Property es una propiedad cuyo valor no se almacena **directamente en el objeto**, sino en un **sistema interno del motor de WPF** llamado **Property System**.

Este tipo de propiedades no son iguales a una propiedad normal de C# en donde se definen como:

```C#
public string Title { get; set; }
```

Las propiedades o **DependencyProperties** se definen como:

```C#
public static readonly DependencyProperty TitleProperty;
```

El valor almacenado se guarda dentro del **sistema de propiedades de WPF** por lo que el campo no lo almacena directamente.

#### Ejemplo de fuentes posibles
| Fuente del valor  | Ejemplo                   |
| ----------------- | ------------------------- |
| Valor local       | `Title="Productos"`       |
| Binding           | `{Binding Nombre}`        |
| Estilo            | `Setter Property="Title"` |
| Animación         | Storyboards               |
| Valor por defecto | Metadata                  |


## Estructura General de una DependencyProperty

+ Un campo **estático** registrado

+ Un wrapper de **propiedad normal**

+ El registro con **`DependencyProperty.Register`**

### Ejemplo de declaración

```C#
//Declaración de la Propiedad
public static readonly DependencyProperty MiPropiedadProperty =
    DependencyProperty.Register(
        "MiPropiedad",
        typeof(string),
        typeof(MiControl),
        new PropertyMetadata("Valor por defecto")
    );

//Wrapper
public string MiPropiedad
{
    get { return (string)GetValue(MiPropiedadProperty); }
    set { SetValue(MiPropiedadProperty, value); }
}
```

Para hacer uso de esta propiedad necesitamos enlazarla mediante un **`Binding`**. El enlace se puede establecer de las siguientes **3 maneras** en **XAML:**

#### 1. Dentro de un Control de Usuario:

```xml
<TextBlock Text="{Binding Title, RelativeSource={RelativeSource AncestorType=UserControl}}"/>
```
#### 2. Dentro de un Control de Usuario pero más común:

```xml
<TextBlock Text="{Binding Title, RelativeSource={RelativeSource AncestorType=local:PanelControl}}"/>
```
Estas formas de declarar el enlace del DependencyProperty se les conoce como **relación de parentesco**.

#### 3. Directamente en el control y mas simplificado:
```xml
<UserControl x:Class="StockMasterControls.PanelControl"
             x:Name="root">

    <Border Background="White" Padding="10">
        
        <StackPanel>
            
            <!-- Aquí es donde se utiliza la "relación de parentesco -->
            <TextBlock
                FontSize="18"
                FontWeight="Bold"
                Text="{Binding Title, ElementName=root}" />

        </StackPanel>

    </Border>

</UserControl>
```

Después de definir y crear el **UserControl** o la relación para hacer uso de nuestra propiedad (en este caso dentro de un **UserControl**) podemos directamente invocar esta misma **DependencyProperty** en donde la usemos mediante **XAML**:
```xml
<controls:UserControl
    Width="400"
    Height="300"
    Title="Gestión de Categorías"/>
```

# Complementos para las Dependency Properties
Todo ocurre dentro del mismo sistema de registro de una Dependency Property. La idea es que **cuando el valor de la propiedad cambia (por XAML, Binding, animación, etc.) el control pueda reaccionar automáticamente.**

## 1. PropertyMetadata
Cuando se registra una **Dependency Property**, el último parámetro del método **`Register`** permite definir **metadata**, es decir, **información adicional sobre la propiedad.** La forma general de declarar esto:
```C#
DependencyProperty.Register(
    "NombrePropiedad",
    typeof(Tipo),
    typeof(ControlPropietario),
    new PropertyMetadata(valorPorDefecto)
);
```
Ese **PropertyMetadata** puede contener:

+ Valor por defecto

+ Callback cuando cambia el valor

+ Otras configuraciones internas del sistema de propiedades

Un ejemplo de como utilizar el **PropertyMetadata:**
```C#
public static readonly DependencyProperty TitleProperty =
    DependencyProperty.Register(
        "Title",
        typeof(string),
        typeof(PanelControl),
        new PropertyMetadata("Titulo por defecto")
    );
```
Esto no ayuda a que si no declaramos el valor de la **DependencyProperty** el **PropertyMetadata** le asigne un valor por defecto.

## 2. DependencyPropertyChangedCallback
Muchas veces no basta con almacenar un valor.
El control necesita **reaccionar cuando la propiedad cambia.** Por ejemplo:

+ actualizar la interfaz

+ recalcular algo

+ ejecutar lógica

Para eso se usa **DependencyPropertyChangedCallback**, en donde se define dentro del **PropertyMetadata.** Su estructura no cambia mas que agrega un parámetro extra que es donde entra el **Método CallBack**:

```c#
new PropertyMetadata(valorPorDefecto, MetodoCallback)
```
La **estructura** del **método CallBack** es la siguiente:
```C#
private static void OnTitleChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
{
    //Código que se ejecuta cuando la propiedad cambia 
}
```
### Descripción de parámetros
| Parámetro          | Significado            |
| ------------------ | ---------------------- |
| **`DependencyObject d`** | instancia del control (Control que usa la propiedad)  |
| **`DependencyPropertyChangedEventArgs e`**| información del cambio |

Dentro del **objeto e** se encuentran datos importantes:
| Propiedad  | Contenido      |
| ---------- | -------------- |
| **`e.OldValue`** | valor anterior |
| **`e.NewValue`** | valor nuevo    |

Un ejemplo aplicando estas funcionalidades del Método **CallBack** son:
```C#
    private static void OnTitleChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        PanelControl control = (PanelControl)d;

        string nuevoValor = (string)e.NewValue;

//Suponiendo que "TitleText" es una propiedad de un UserControl enlazado a un TextBlock. Cuando este se le asigna un nuevo valor el TextBlock actualiza su contenido.
        control.TitleText.Text = nuevoValor;
    }
```

## Conclusión
Las **Dependency Properties** permiten que un control:
+ reciba valores desde XAML

+ funcione con Binding

+ soporte animaciones

+ responda automáticamente a cambios

```Markdown
**El mecanismo de reacción es:**

PropertyMetadata
       ↓
DependencyPropertyChangedCallback
       ↓
Control reacciona automáticamente
```

