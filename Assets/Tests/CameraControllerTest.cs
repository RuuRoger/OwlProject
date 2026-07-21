using NUnit.Framework;
using UnityEngine;
using Assets.Core.Features.Camera.Controller;

public class CameraControllerTest
{
    /*
        - Tipo Movimiento
            Seguir al player con offset
        - Fórmula:
            nuevaPosicionCamara = posicionPlayer + offsetCamara
        - Tipo de dato:
            vector3 = vector3 + vector3
        DATOS:
            posicionPlayer,
            offsetCamara,
            nuevaPosicionCamara
    */

    // No permite la etiqueta testcase pasar vecotres!!!
    [TestCase(
        1f, 0f, 0f,       // posicionPlayer (X, Y, Z)
        0f, 5f, -10f,     // offsetCamara (X, Y, Z)
        1f, 5f, -10f      // nuevaPosicionCamara esperada (X, Y, Z)
    )]

    public void Unitario_Camara_CalcularElOffset(
        float playerX, float playerY, float playerZ, 
        float offsetX, float offsetY, float offsetZ, 
        float esperadoX, float esperadoY, float esperadoZ
    )
    {
        var posicionPLayer = new Vector3(playerX, playerY, playerZ);
        var offsetCamara = new Vector3(offsetX, offsetY, offsetZ);
        var nuevaPosicionCamara = new Vector3(esperadoX, esperadoY, esperadoZ);
        var rotacionSinGirar = Quaternion.identity;

        Vector3 resultado = CameraController.CalcularPosicion(posicionPLayer, rotacionSinGirar, offsetCamara);

        Assert.AreEqual(nuevaPosicionCamara.x, resultado.x, 0.001f);
        Assert.AreEqual(nuevaPosicionCamara.y, resultado.y, 0.001f);
        Assert.AreEqual(nuevaPosicionCamara.z, resultado.z, 0.001f);
    }

    /*
        Tipo Movimiento
            Rotación de la cámara a modo de "look at"
        Fórmula:
            ** direccionRotacion = posicionPlayer - posicionCamara
            ** quaternion = (x * sin(alpha/2), y * sin(alpha/2), z * sin(alpha/2), cos(alpha/2))
        Tipo de dato:
            vector3 = vector3 - vector3
        Datos:
            posicionPlayer,
            posicionCamara
    */
    [TestCase(
        0f, 0f, 0f,       // posicionPlayer (X,Y,Z)
        0f, 5f, -5f,      // posicionCamara (X,Y,Z)
        0.3826834f, 0f, 0f, 0.9238795f // Quaternion esperado (X, Y, Z, W) para inclinación de 45°
    )]
    public void Unitario_Camara_LookAt(
        float playerX, float playerY, float playerZ,
        float camaraX, float camaraY, float camaraZ,
        float esperadoX, float esperadoY, float esperadoZ, float esperadoW
    )
    {
        var posicionPlayer = new Vector3(playerX, playerY, playerZ);
        var posicionCamara = new Vector3(camaraX, camaraY, camaraZ);
        var rotacionEsperada = new Quaternion(esperadoX, esperadoY, esperadoZ, esperadoW);

        Quaternion rotacionObtenida = CameraController.CalcularRotacion(posicionCamara, posicionPlayer);

        float diferenciaAngulo = Quaternion.Angle(rotacionEsperada, rotacionObtenida);

        Assert.LessOrEqual(diferenciaAngulo, 0.1f);
    }
}