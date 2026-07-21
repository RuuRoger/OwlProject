using UnityEngine;

namespace Assets.Core.Features.Player.Controller
{
    public static class PlayerController
    {
        // public static Vector3 CalcularMovimiento(
        //     Vector3 posicionActual,
        //     Vector2 direccionBruta,
        //     float velocidad,
        //     float tiempo
        // )
        // {
        //     Vector2 direccionNormalizada = NormalizarMovimientoDiagonal(direccionBruta);
        //     Vector3 direccionEnMundo = new Vector3(direccionNormalizada.x, 0, direccionNormalizada.y);
            
        //     return MovimientoHorizontalVertical(posicionActual, direccionEnMundo, velocidad, tiempo);
        // }

        /// <summary>
        /// Calcula la posición final del player con el método de reposicionar el Transform del GameObject, retornando un vector 3 de la nueva posición
        /// </summary>
        /// <param name="posicionActual"></param>
        /// <param name="direccion"></param>
        /// <param name="velocidad"></param>
        /// <param name="tiempo"></param>
        /// <returns>Vector3</returns>
        public static Vector3 DesplazamientoPlayer(Vector3 posicionActual, Vector3 direccion, float velocidad, float tiempo)
        {
            return posicionActual + (tiempo * velocidad * direccion);
        }

        public static float RotacionSobreEjeY(float rotacionInicial, float inputX, float velocidadRotacion, float tiempo)
        {
            return rotacionInicial + (inputX * velocidadRotacion * tiempo);
        }

    }
}