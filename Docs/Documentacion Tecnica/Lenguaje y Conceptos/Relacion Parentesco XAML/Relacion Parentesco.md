# Relative Resource o Relación Parentesco.
Cuando se tienen controles como **botones** dentro de un **borde**, que está dentro de una **cuadrícula**, que está dentro de un **UserControl**, Si el **botón quiere saber el color de fondo del UserControl**, no puede usar un **Binding** normal porque no conoce el nombre de ese control. **`RelativeSource`** permite que el botón acceda a traves de un árbol visual hasta que se encuentre un control padre definido en las reglas del **`RelativeSource`**.

## Desglosando la sintaxis
En la declaración **`RelativeSource={RelativeSource AncestorType=UserControl}`** cada palabra clave tiene funciones. Se describen estas funciones como:

**`RelativeSource=`**: Propiedad que **instruye al Binding** en que el **origen** no es un **objeto estático**, sino un **padre** o **control "cercano".**

**`{RelativeSource ...}`**: Es la **extensión** de marcado que **define las reglas de búsqueda**.

**`AncestorType=UserControl`**: Es el **filtro** donde se indica a la **extensión Seguir la jerarquía** hasta encontrar el primer elemento **definido en las reglas de búsqueda (UserControl,border,button,grid,etc)**.

## Opciones para los Modos de `RelativeSource`
El **`AncestorType`** puede **configurarse**:


| **Modo** | **Funcionalidad** | **Ejemplo de uso** |
|----------|-------------------|--------------------|
| **`FindAncestor`** | Busca hacia arriba en el árbol visual un tipo específico.| **`AncestorType=Window`** para llegar a la ventana principal. |
| **`Self`** | Se conecta a una propiedad del mismo elemento. | Si quieres que el ancho de un botón sea igual a su propia altura. |
| **`TemplatedParent`** | Se usa dentro de un **`ControlTemplate`**. | Para conectar una propiedad del diseño (Template) con el control real. |
| **`PreviousDate`** | Se usa en listas (**ItemsControl**). | Para comparar el valor de un elemento con el anterior en la lista. |

Este **servicio** o **característica** del **framework** es muy común porque para diseño de controles personalizado. Como extra si colocamos **`AncestorLevel=2`**, podemos movernos al primer ancestro y buscar al segundo que coincida con el **tipo**.