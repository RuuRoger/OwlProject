using NUnit.Framework;
using UnityEngine;
using Assets.Core.Features.Player.Controller;

public class PlayerControllerTest
{
    /*
        - Tipo Movimiento
            El movimiento se hará en Unity con Transform.
            El movimiento es tipo tanque, por lo que solo evaluaré el movimiento en el eje Z            
        - La formula a aplicar:
            posicionFinal = posicionInicial + desplazamientoZ = posicionInicial + (InputY * direccionZ * velocidad * tiempo)
        - Tipo de dato:
            (1) => vector3 = vector3 + vector3 = vector3 + (escalar * vector3 * escalar * escalar)
            (2) => metros = metros + metros = metros + (escalar adimensional * vector adimensional (está normalziado) * metros/segundos * segundos)

        DATOS:
            posicionPlayerZ,
            direccionInputY,
            velocidad,
            tiempo,
            posicionFinalPlayerZ
    */
    [TestCase(0f, 1f,  5f, 1f, 5f)] // Moverse adelante
    [TestCase(0f, -1f, 5f, 1f, -5f)] // Moverse atrás
    [TestCase(0f, 0f,  0f, 1f, 0f)] // Quedarse quieto

    public void Unitario_Player_MovimientoEnEjeZ_CalculaLaPosicion(float posicionPlayerZ, float direccionInputY, float velocidad, float tiempo, float posicionFinalPlayerZ)
    {
        var posicionInicial = new Vector3(0f, 0f, posicionPlayerZ);
        var direccionInput2D = new Vector2(0f, direccionInputY);
        var direccionPlayerGlobal = new Vector3(0f, 0f, direccionInput2D.y);

        var posicionPlayerFinal = PlayerController.DesplazamientoPlayer(posicionInicial, direccionPlayerGlobal, velocidad, tiempo);

        Assert.AreEqual(0f, posicionPlayerFinal.x, 0.0001f);
        Assert.AreEqual(posicionFinalPlayerZ, posicionPlayerFinal.z, 0.0001f);
    }

    /*
        Fórmula rotación (para el eje Y): 
            rotacionFinalY = rotacionInicalY +  deltaRotacion = rotacionInicialY + (InputX * velocidadRotacion * tiempo)
        Tipo de dato:
            grados = gradps + grados = grados + (escalar adimensional * grados/segundos * segundos)
        DATOS:
            rotacionPlayerY,
            InputX,
            velocidadRotacion,
            tiempo,
            rotacionFinalY
    */
    [TestCase(0f, 0f, 5f, 1f, 0f)] // Sin rotación
    [TestCase(0f, -1f, 5f, 1f, -5f)] // Rotación izquierda local
    [TestCase(0f, 1f, 5f, 1f, 5f)] // Rotación derecha local

    public void Unitario_Player_RotacionY(float rotacionPlayerY, float inputX, float velocidadRotacion, float tiempo, float rotacionFinalY)
    {
        var rotacionInicalY = new Vector3(0f, rotacionPlayerY, 0f);
        var rotacionInput2D = new Vector2(inputX, 0f);
        
        var rotacionFinal = PlayerController.RotacionSobreEjeY(rotacionPlayerY, inputX, velocidadRotacion, tiempo);

        Assert.AreEqual(rotacionFinalY, rotacionFinal, 0.0001f);
    }


}                                                                                                                                                                       