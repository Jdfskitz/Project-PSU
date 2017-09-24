/* This file handles the storage off all the Data Type Based ENUM's these are used to assign variables and check status for variables.
 * We tried to cover as many bases as we could if there is something you feel is too much or would like us to add or remove in future releases please contact us
 * or post on our forums regarding this request. We want to make it the most rebust system possible so let us know if there is something you would like to be added
 * we will try our best to add/change whatever is in popular demand.
 * 
 * These ENUMS will be explained of their purpose at the tops in comments.
 * 
 * */

// THIS IS THE NAMESPACE REQUIRED TO ACCESS THE MDF DATABASE FRAMEWORK

namespace MDF_EDITOR
{
    // This is to choose field type, currently we have only VALUE and REFERENCE but this will expand further into more types!
    // for Example: You make a variable with the VALUE type, this will show the next ENUM VALUE_DATATYPE. as a dropdown to select in the GUI
    /// <summary>
    /// This is to choose field type, currently we have only VALUE and REFERENCE but this will expand further into more types!
    /// <para>for Example: You make a variable with the VALUE type, this will show the next ENUM VALUE_DATATYPE. as a drop down to select in the GUI</para>
    /// </summary>
    public enum SELECTED_TYPE
    {
        NONE,
        VALUE,
        REFERENCE
    }

    /// <summary>
    ///  These are VALUE Data Type Variables, variables that STORE data themselves!
    /// </summary>
    public enum VALUE_DATATYPE //TODO: Add proper support for commented out VALUE types.
    {
        NONE,
        STRING,
//		CHAR,
        INT,
        LONG,
        DOUBLE,
        FLOAT,
//		SHORT,
//		BYTE,
        BOOL,
        VECTOR2,
        VECTOR3,
        VECTOR4,
//		QUATERNION,
//		RAY,
//		RAY2D,
        RECT,
        COLOR,
        COLOR32
    }

    /// <summary>
    /// These are REFERENCE Data Type Variables, These refer to objects or other scripts to handle specific types, like GameObjects or Components
    /// </summary>
    public enum REFERENCE_DATATYPE //TODO: Add proper support for commented out VALUE types.  
    {
        NONE,
        OBJECT,
        GAMEOBJECT,
        TRANSFORM,
        COMPONENT,
        CAMERA,
        LIGHT,
        SCRIPTABLE_OBJECT,
        AVATAR,
        CHARACTER_CONTROLLER,
        ANIMATION,
        ANIMATION_CLIP,
        ANIMATIOR,
        AUDIO_CLIP,
        AUDIO_SOURCE,
        AUDIO_LISTENER,
        COLLIDER,
        COLLIDER_2D,
        BOX_COLLIDER,
        BOX_COLLIDER_2D,
        CAPSULE_COLLIDER,
        EDGE_COLLIDER_2D,
        POLYGON_COLLIDER2D,
        MESH_COLLIDER,
        TERRIAN_COLLIDER,
        WHEEL_COLLIDER,
//		COLLISION,
//		COLLISION_2D,
        PHYSIC_MATERIAL,
        PHYSICS_MATERIAL_2D,
//		PHYSICS,
//		PHYSICS_2D,
        RIGIDBODY,
        RIGIDBODY_2D,
        EFFECTOR_2D,
        PLATFORM_EFFECTOR_2D,
        SURFACE_EFFECTOR_2D,
        AREA_EFFECTOR_2D,
        POINT_EFFECTOR_2D,
        FONT,
        SPRITE,
        SPRITE_RENDERER,
        MESH,
        CUBEMAP,
        MATERIAL,
        TEXTURE,
        TEXTURE_2D,
        TEXTURE_3D,
        SPARSE_TEXTURE,
        SHADER,
        CLOTH,
        SKYBOX
    }

    // This is the LIST type selector, NONE means, Single Variable, no list. and then you have all your standard LIST types
    // NOTE in order to use these you NEED The namespaces as they appear below in all scripts OUTSIDE of the MDF Framework

    // using System.Collections;
    // using System.Collections.Generic;
    //
    //
    /// <summary>
    /// This will all you to use the List types properly =), these will be generated automatically if you generate scripts with the GUI
    /// </summary>
    public enum LIST_TYPE
    {
        NONE,
        ARRAY,
        LIST
		//SORTED_LIST, //TODO: Add Sorted list support
		//DICTIONARY, //TODO: Add Dictionary support
		//SORTED_DICTIONARY, //TODO: Add Sorted Dictionary support
        //ENUM, //TODO: Add Enum support
        //HASHTABLE //TODO: Add HashTable support
    }
}