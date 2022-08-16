using Mirror;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

public class CursorManager : NetworkBehaviour
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public static implicit operator Point(POINT point) => new Point(point.X, point.Y);
    }

    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll")]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
    
    private const int MOUSEEVENTF_LEFTDOWN = 0x02;
    private const int MOUSEEVENTF_LEFTUP = 0x04;
    private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const int MOUSEEVENTF_RIGHTUP = 0x10;

    [SerializeField] private float _speed = 300f;

    [Server]
    public void MoveCursor(Vector2 direction)
    {
        Vector2Int pos = GetCursorPosition() + Vector2Int.RoundToInt(direction.normalized * _speed * Time.deltaTime);

        SetCursorPos(pos.x, pos.y);
    }

    [Server]
    public void LeftMouseButtonClick()
    {
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }

    [Server]
    public void RightMouseButtonClick()
    {
        mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
        mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
    }
    
    private Vector2Int GetCursorPosition()
    {
        GetCursorPos(out var pos);

        return new Vector2Int(pos.X, pos.Y);
    }
}
