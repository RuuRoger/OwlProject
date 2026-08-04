# Adventures of Carol

Un desarrollo de videojuego RPG por turnos con perspectiva 2.5D. Actualmente se encuentra en una fase **muy inicial y de prototipado**. 

⚠️ **OBJETIVO DEL PROYECTO**

El valor real de este proyecto radica en su arquitectura interna:
* **Lógica Core Desacoplada:** Implementación de un flujo de trabajo utilizando **C# puro** para el núcleo del juego, permitiendo un entorno robusto para pruebas unitarias rápidas y flujos de diseño TDD aislados de los componentes nativos de Unity (`MonoBehaviour`).
* **MVC + Patrón Mediador:** Aplicación de la arquitectura MVC utilizando **ScriptableObjects** como modelo, scripts **MonoBehaviour** como vista y controladores en **C# puro**, coordinados mediante un patrón mediador para facilitar el mantenimiento y el desacoplamiento.
* **Gráficos Técnicos a Mano:** Desarrollo del comportamiento visual y estética técnica escribiendo código **HLSL puro** directamente (iluminación, efectos, partículas, etc.).
* **Código Limpio y Git:** Mostrar la estructuración de scripts, organización del proyecto y un flujo de trabajo con Git basado en ramas por *features* y *commits* descriptivos.

El fin principal de este repositorio **no es desarrollar un videojuego completo**, sino servir como entorno técnico de aprendizaje y experimentación personal. No se descarta finalizar el juego si el proyecto muestra potencial.

---

⚠️ **Conclusiones del prototipo**

Uno de los principales aprendizajes ha sido comprobar la fricción que genera forzar a Unity a trabajar fuera de su paradigma natural (programación orientada a componentes). 

Mi objetivo era tener mayor control con pruebas unitarias usando NUnit y no depender tanto del motor y usar una arquitectura MVC. Sin embargo, para insistir en una arquitectura tan desacoplada, habría sido más orgánico trabajar con motores o frameworks más minimalistas o "crudos" (como **MonoGame** o **Stride**). No obstante, esta prueba me ha servido para valorar las facilidades nativas de Unity y tomar mejores decisiones según la necesidad del proyecto.

En cuanto al apartado visual 2.5D (estilo *Paper Mario*), he comprobado que funciona bien con ciertos elementos aislados (como árboles), pero pierde el efecto con estructuras más complejas como casas.

**Puntos no integrados en este prototipo:**
- No se ha realizado los shaders en HLSL.
- La adaptación de MVC con patrón mediador para componentes no quedó integrada como estaba planeada inicialmente.

**Logros positivos:**
- Pruebas unitarias integradas con **NUnit** sobre C# puro.
- Implementación de un sistema de diálogo básico funcional.
- Control y flujo de combate por turnos.
- Comprensión de la arquitectura interna de Unity y evaluación de alternativas.

---

## 🎮 Sobre el Proyecto

En **Adventures of Carol** se plantean las bases para acompañar a Carol y a su *party* en un mundo que combina mecánicas de rol clásico, combates estratégicos por turnos y un entorno visual 2.5D (sprites 2D integrados en un espacio tridimensional).

---

## 🛠️ Detalles Técnicos

* **Motor de Videojuego:** Unity 6 LTS
* **Sistema de Entrada:** Unity Input System (guiado por eventos)
* **Programación Gráfica:** HLSL *(en fase de estudio/desarrollo)*
* **Arquitectura:** Component-Based + Patrón Mediador (orquestación mediante `PlayerManager`)
* **Perspectiva Visual:** 2.5D (Sprites Pixel Art en espacio 3D)

---

## 🎨 Créditos y Recursos Visuales

Los recursos gráficos utilizados en este prototipo pertenecen a sus respectivos creadores:

* **Sprites del Personaje Principal (Carol):**
  * Autor: `[SSCARY]`
  * Perfil en Itch.io: [Visitar sitio](https://sscary.itch.io/)
  * Recurso: [The Adventurer Female](https://sscary.itch.io/the-adventurer-female)

---

## 🚀 Estado del Desarrollo

- 🟡 Prototipando / Investigación técnica
