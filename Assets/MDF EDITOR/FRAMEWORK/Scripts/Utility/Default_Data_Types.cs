using UnityEngine;

namespace MDF_EDITOR
{
    /// <summary>
    /// This class handles default types for several types and is used for assigning default values. //Todo: currently not used? Should we remove or make this the new reference point.
    /// </summary>
    public static class DEFAULT_DATATYPE_VALUES
    {
        //BELOW ARE THE DEFAULTS FOR DATA TYPES THAT CAN BE USED AS A REFERENCE!
        //VALUE TYPE DEFUALTS
        public static int DEFAULT_ID = -1;
        public static string DEFAULT_NAME = "NONE";
        public static string DEFAULT_STRING = "";
        public static char DEFAULT_CHAR = 'a';
        public static int DEFAULT_INT = 0;
        public static long DEFAULT_LONG = 0;
        public static double DEFAULT_DOUBLE = 0;
        public static float DEFAULT_FLOAT = 0;
        public static short DEFAULT_SHORT = 0;
        public static byte DEFAULT_BYTE = 0;
        public static bool DEFAULT_BOOL = false;
        public static Vector2 DEFAULT_VECTOR2 = new Vector2(0, 0);
        public static Vector3 DEFAULT_VECTOR3 = new Vector3(0, 0, 0);
        public static Vector4 DEFAULT_VECTOR4 = new Vector4(0, 0, 0, 0);
        public static Quaternion DEFAULT_QUATERNION = new Quaternion(0, 0, 0, 0);
        public static Ray DEFAULT_RAY = new Ray(new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        public static Ray2D DEFAULT_RAY2D = new Ray2D(new Vector2(0, 0), new Vector2(0, 0));
        public static Rect DEFAULT_RECT = new Rect(0, 0, 0, 0);
        public static Color DEFAULT_COLOR = new Color(0, 0, 0);
        public static Color32 DEFAULT_COLOR32 = new Color32(0, 0, 0, 0);

        //REFERENCE TYPE DEFAULTS //
        public static GameObject GAMEOBJECT_DEFAULT = null;
        public static Transform TRANSFORM_DEFAULT = null;
        public static object OBJECT_DEFAULT = null;
        public static Camera CAMERA_DEFAULT = null;
        public static Light LIGHT_DEFAULT = null;
        public static Component COMPOENT_DEFAULT = null;
        public static ScriptableObject SCRIPTABLEOJECT_DEFAULT = null;
        public static Avatar AVATAR_DEFAULT = null;
        public static CharacterController CHARACTERCONTROLLER_DEFAULT = null;
        public static Animation ANIMATION_DEFAULT = null;
        public static AnimationClip ANIMATIONCLIP_DEFAULT = null;
        public static Animator ANIMATOR_DEFAULT = null;
        public static AudioClip AUDIOCLIP_DEFAULT = null;
        public static AudioSource AUDIOSOURCE_DEFAULT = null;
        public static AudioListener AUDIOLISTENER_DEFAULT = null;
        public static Collider COLLIDER_DEFAULT = null;
        public static Collider2D COLLIDER2D_DEFAULT = null;
        public static BoxCollider BOXCOLLIDER_DEFAULT = null;
        public static BoxCollider2D BOXCOLLIDER2D_DEFAULT = null;
        public static CapsuleCollider CAPSULECOLLIDER_DEFAULT = null;
        public static EdgeCollider2D EDGECOLLIDER2d_DEFAULT = null;
        public static PolygonCollider2D POLYGONCOLLIDER2d_DEFAULT = null;
        public static MeshCollider MESHCOLLIDER_DEFAULT = null;
        public static TerrainCollider TERRAINCOLLIDER_DEFAULT = null;
        public static WheelCollider WHEELCOLLIDER_DEFAULT = null;
        public static Collision COLLISION_DEFAULT = null;
        public static Collision2D COLLISON2D_DEFAULT = null;
        public static PhysicMaterial PHYSICMATERIAL_DEFAULT = null;
        public static PhysicsMaterial2D PHYSICSMATERIAL2D_DEFAULT = null;
        public static Physics PHYSICS_DEFAULT = null;
        public static Physics2D PHYSICS2D_DEFAULT = null;
        public static Rigidbody RIGIDBODY_DEFAULT = null;
        public static Rigidbody2D RIGIDBODY2D_DEFAULT = null;
        public static Effector2D EFFECTOR2D_DEFAULT = null;
        public static PlatformEffector2D PLATFORMEFFECTOR2D_DEFAULT = null;
        public static SurfaceEffector2D SURFACEEFFECTOR2D_DEFAULT = null;
        public static AreaEffector2D AREAEFFECTOR2D_DEFAULT = null;
        public static PointEffector2D POINTEFFECTOR2D_DEFAULT = null;
        public static Font FONT_DEFAULT = null;
        public static Sprite SPRITE_DEFAULT = null;
        public static SpriteRenderer SPRITERENDERER_DEFAULT = null;
        public static Mesh MESH_DEFAULT = null;
        public static Cubemap CUBEMAP_DEFAULT = null;
        public static Material MATERIAL_DEFAULT = null;
        public static Texture TEXTURE_DEFAULT = null;
        public static Texture2D TEXTURE2D_DEFAULT = null;
        public static Texture3D TEXTURE3D_DEFAULT = null;
        public static SparseTexture SPARSETEXTURE_DEFAULT = null;
        public static Shader SHADER_DEFAULT = null;
        public static Cloth CLOTH_DEFAULT = null;
        public static Skybox SKYBOX_DEFAULT = null;
    } // END DEFAULT_DATATYPE_VALUES CLASS
} // END MDF_EDITOR NAMESPACE