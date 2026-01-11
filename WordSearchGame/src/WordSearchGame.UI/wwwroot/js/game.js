window.game = {
    releaseCapture: function (pointerId) {
        // Try to release capture from likely targets?
        // Actually, we can just release it from the element that has it.
        // But we don't know which one has it in JS easily without tracking.
        // Better: Pass the element ID.
    },
    releaseCaptureByRowCol: function (r, c, pointerId) {
        const el = document.querySelector(`.cell[data-row="${r}"][data-col="${c}"]`);
        if (el && el.releasePointerCapture) {
            el.releasePointerCapture(pointerId);
        }
    }
};
