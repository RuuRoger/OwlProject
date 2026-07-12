# OwlProject (Nombre Clave)

Un desarrollo de videojuego RPG por turnos con perspectiva 2.5D. Actualmente se encuentra en una fase **muy inicial y de prototipado**. 

⚠️ **OBJETIVO DEL PROYECTO:** 

El valor real del proyecto radica en su arquitectura interna:
* **Lógica Core Desacoplada:** Implementación de un flujo de trabajo optimizado utilizando **C# puro** para el núcleo del juego, permitiendo un entorno robusto para pruebas unitarias rápidas y flujos de diseño TDD aislados de los componentes nativos de Unity (`MonoBehaviour`).
* **MVC + Patrón Mediador:** Aplicar la arquitectura MVC con **ScripteableObjects** como modelo, scripts **MonoBehaviour** como vista y scripts de **"C# puro""** como controladores, trabajando a su vez con el patrón mediador, para faciltiar el mantenimiento y mejorar el desacoplamiento.
* **Gráficos Técnicos a Mano:** Todo el comportamiento visual: efectos de renderizado y estética técnica, se desarrollarán escribiendo código **HLSL puro** directamente (Iluminación, explosiones, humo, líquidos...).
* **Exponer el Código y Git:** Mostrar cómo escribo/pienso el código y estructuro los scripts, cómo organizo las carpetas y archivos, además de trabajar con features intentando realizar ramas limpias y commits correctos.

El fin principal de este repositorio **no es desarrollar un videojuego completo**, sino servir como entorno técnico de aprendizaje y experimentación personal con las estructuras mencionadas y el desarrollo en HLSL.

No se descarta finalizar el videojuego si se percibe potencial en el proyecto.

---

## 🎮 Sobre el Proyecto

En **OwlProject** (nombre clave), se plantean las bases para acompañar a **Carol** y a su *party* en una serie de aventuras a través de un mundo que combina mecánicas de rol clásico, combates estratégicos por turnos y un entorno 2.5D (intersección de planos 2D en espacio tridimensional).



---

## 🛠️ Detalles Técnicos

* **Motor de Videojuego:** Unity 6.3 LTS (6000.3.12f1)
* **Sistema de Entrada:** Unity Input System (Paquete guiado por eventos)
* **Programación Gráfica:** Shaders custom escritos a mano en **HLSL**
* **Arquitectura:** Component-Based Architecture + Patrón Mediador (para la orquestación de componentes mediante un PlayerManager)
* **Perspectiva Visual:** 2.5D (Sprites Pixel Art orientados en espacio 3D)

---

## 🎨 Créditos y Recursos Visuales

Los recursos gráficos utilizados en este prototipo inicial pertenecen a sus respectivos creadores. A continuación se listan las fuentes y enlaces oficiales:

* **Sprites del Personaje Principal (Caro):**
    * Autor: `[SSCARY]`
    * Enlace a su Itch.io: [Visitar sitio](https://sscary.itch.io/)
    * Enlace al recurso: [Visitar sitio](https://sscary.itch.io/the-adventurer-female)
    
---

## 🚀 Estado del Desarrollo

- Prototipando
