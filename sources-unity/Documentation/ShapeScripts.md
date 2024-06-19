**ShapeScriptz Documentation**

**Overview:**

ShapeScriptz is a simple scripting language designed for defining custom shapes in Unity. It allows users to specify a list of vertices, which are then used to create a custom shape in the Unity environment. This documentation provides an overview of the syntax and usage of ShapeScriptz.

**Syntax:**

A ShapeScriptz file consists of a series of commands and parameters, organized into sections. Each section defines a specific aspect of the custom shape, such as vertices, rotations, or scaling.

**Sections:**

1. **Vertices (P):**  
   - Syntax: `P: (x,y)`
   - Description: Defines a vertex point in 2D space with coordinates `(x,y)`.

2. **Object:**
   - Syntax: `Object`
   - Description: Signals the end of vertex definitions and indicates that the custom shape should be created.

3. **Rotate:**
   - Syntax: `Rotate (angle)`
   - Description: Specifies a rotation angle (in degrees) for the custom shape. Rotation occurs around the anchor point.

4. **Place:**
   - Syntax: `Place (x,y)`
   - Description: Specifies a new placement position for the custom shape. This command can be used to move the shape within the Unity environment.

5. **Scale:**
   - Syntax: `Scale (factor)`
   - Description: Specifies a scaling factor for the custom shape. This command can be used to resize the shape.

**Usage:**

1. **Defining Vertices:**
   - Use the `P:` command followed by coordinates `(x,y)` to define each vertex.
   - Vertices should be listed in clockwise or counterclockwise order to properly define the shape.

2. **Creating the Shape:**
   - Use the `Object` command to signal the end of vertex definitions and create the custom shape.

3. **Modifying the Shape:**
   - Optionally, use the `Rotate`, `Place`, and `Scale` commands to modify the shape's rotation, position, and size, respectively.

**Example:**

```plaintext
# Rectangle Example
P: (-6, -2)   # Bottom-left corner
P: (-6, 2)    # Top-left corner
P: (-2, 2)    # Top-right corner
P: (-2, -2)   # Bottom-right corner
Object

# Square Example
P: (-1, -1)   # Bottom-left corner
P: (-1, 1)    # Top-left corner
P: (1, 1)     # Top-right corner
P: (1, -1)    # Bottom-right corner
Object

# Hexagon Example
P: (2, 0)         # Bottom-left corner
P: (5.4, 4.503)   # Top-left corner
P: (10.2, 4.503)  # Top corner
P: (13.6, 0)      # Bottom-right corner
P: (10.2, -4.503) # Bottom corner
P: (5.4, -4.503)  # Top-right corner
Object
```

This example defines a hexagon shape with vertices at specified coordinates. Once the `Object` command is encountered, the hexagon is created in the Unity environment.

**File Format:**

- ShapeScriptz files should have a `.shz` extension.
- They can be read as plain text files and edited using any text editor.

**Implementation Details:**

- The ShapeScriptz interpreter is implemented in C# within a Unity project.
- It parses the script file, processes the commands, and generates the corresponding custom shape using Unity's GameObject and Mesh functionalities.

**Conclusion:**

ShapeScriptz provides a straightforward way to define custom shapes in Unity using a simple scripting language. By specifying vertices and additional parameters, users can create and manipulate shapes within their Unity projects efficiently.