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
    /// <summary>
    /// These are the keys that can be pressed down. Used when setting up shortcuts for database menus
    /// </summary>
    public enum HOTKEY
    {
        NONE,
        CTRL_or_CMD,
        SHIFT,
        ALT
//		LEFT,
//		RIGHT,
//		UP,
//		DOWN,
//		HOME,
//		PAGE_UP,
//		PAGE_DOWN,
//		END,
//		F1,
//		F2,
//		F3,
//		F4,
//		F5,
//		F6,
//		F7,
//		F8,
//		F9,
//		F10,
//		F11,
//		F12
    }
}