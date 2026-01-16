using UnityEngine;
using UnityEngine.EventSystems; // Wajib untuk deteksi sentuhan

public class MobileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public bool isPressed = false;

    // Dipanggil saat jari menyentuh tombol
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    // Dipanggil saat jari melepas tombol
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}