# Windows Forms Image Processing Application

A Windows Forms application developed in **C#** for image processing. The application allows users to load images, select specific regions, apply gradient colors or colormaps, and save the processed images.

---

## Features

### User Interface
- **Top Buttons**:
  - **File**:
    - **Open**: Load an image from your disk and display it in `PictureBox1` for editing.
    - **Select Area**: Enable region selection on the image.
      - **Mouse Events**:
        - `MouseDown`: Records the starting point of selection.
        - `MouseMove`: Updates selection rectangle dimensions.
        - `MouseUp`: Finalizes selection and resets cursor.
    - **Save**: Save the current processed image(s).
  - **Coloring**:
    - **Color Area**: Apply a gradient color to the selected region.
      - Uses `ApplyGradientColorToSelectedArea(Color selectedColor)` method.
      - Adjusts pixel colors based on brightness.
    - **Colormap Options**:
      - **JetColormap**: Blue → Green → Yellow → Red gradient.
      - **RainbowColormap**: Full rainbow spectrum divided into 5 color segments.
      - **InfernoColormap**: Black → Red → Orange → Yellow → White gradient.

---

## How It Works

### Gradient Coloring
1. Retrieve current pixel color.
2. Calculate brightness as the average of RGB values.
3. Compute gray value inversely proportional to brightness.
4. Modify RGB values based on selected color and gray value.
5. Apply new color to pixel and update `PictureBox`.

### Colormap Processing
- Generates a copy of the current image to preserve the original.
- Iterates through each pixel:
  1. Computes pixel brightness.
  2. Normalizes brightness to 0–1 range.
  3. Maps brightness to corresponding color in the selected colormap (`Jet`, `Rainbow`, or `Inferno`).
  4. Updates pixel color in the image.
- Sets the processed image to `PictureBox1` and refreshes display.

---

## Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/Balkis-Gharzaldein/ImageProcessingApp.git
