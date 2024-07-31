using UnityEngine;

namespace Infrastructure.Services.InputService
{
    public class PCInputService : InputService
    {
        public override bool IsPaintClick() =>
            Input.GetMouseButtonDown(0);

        public override bool IsPaintHold() =>
            Input.GetMouseButton(0);

        public override bool IsPaintUp() =>
            Input.GetMouseButtonUp(0);
    }
}