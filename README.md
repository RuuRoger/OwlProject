# Adventures of Carol

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
⚠️ ** Conclusiones del prototipo presentado **
Ha sido un error forzar a Unity en trabajar en una arquitectura distinta a la suya. Unity está pensado para trabajar con programación orientada a componentes y facilita mucho el trabajo en "surfear" la arquitectura plantada de esta forma.
Mi objetivo era poder tener un mayor control para pruebas unitarias con Nunit y no depender tanto del motor, pero quizás, hubiese sido mejor (en caso de insistir con esta arquitectura), en trabajar con otros motores mas minimalistas como MonoGame o Strade.
No obstante, este proyecto me ha hecho valorar Unity como motor y tomar mejores decisiones para adaptar mi forma de trabajar o la necesidad del proyecto y con algo de acierto pero con confusión, en mi idea inicial en que no solo se tiene o se puede trabajar con Unity y Unreal Engine.

Otro de mis objetivos era poder trabajar con un videojuego 2.5D y adaptar la cámara a este tipo de diseño, buscar un estilo "Paper Mario". He descubierto que es posible con cierto tipo de formas (como los arboles en el juego), pero con otro tipo de objetos como casas, pierde el efecto...

No he podido plasmar conocimientos eh HLSL para este prototipo.
Tampoco he conseguido adaptar la arquitectura MVC con un patrón mediador para los componentes como yo tenía pensado.

No obstante, estoy contento por:
- Haber podido trabajar con pruebas unitarias en Nunit con C# "puro"
- Retos del proyecto como crear un sistema de diálogo básico
- Controlar el combate por turnos
- Valorar la arquitectura de Unity y las facilidades que ofrece si se fluye con su modo de trabajar
- Valorar que Unity no es la única alternativa y que mi planteamiento inicial hubiese sido posible en otros motores más "crudos"

## 🎮 Sobre el Proyecto

En Adventures of Carol, se plantean las bases para acompañar a **Carol** y a su *party* en una serie de aventuras a través de un mundo que combina mecánicas de rol clásico, combates estratégicos por turnos y un entorno 2.5D (intersección de planos 2D en espacio tridimensional)
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

* **Sprites del Personaje Principal (Carol):**
    * Autor: `[SSCARY]`
    * Enlace a su Itch.io: [Visitar sitio](https://sscary.itch.io/)
    * Enlace al recurso: [Visitar sitio](https://sscary.itch.io/the-adventurer-female)
    
---

## 🚀 Estado del Desarrollo

- Prototipando
