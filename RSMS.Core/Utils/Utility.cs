using System;
using System.Collections;
using Optics.Core.Utils;
using System.Windows.Forms;

/// <summary>
/// Summary description for Utility
/// </summary>
public class Utility
{
    public Utility()
    {

    }

    /// <summary>
    /// This method is used to log errors if any while application is being used.
    /// </summary>
    /// <param name="error">This will contain precise error message (ex.message)</param>
    /// <param name="errorDesc">This parameter will contain description of the error(ex.toString())</param>
    /// <param name="functionName">This parameter will contain the name of the function where error has occurred.</param>
    /// <param name="className">This parameter will contain the name of the class in which the function resides where error has occurred.</param>

    public static void LogError(string error, string errorDesc, string functionName, string className)
    {
        Hashtable htParams = new Hashtable();
        htParams.Add("@ErrorMessage", error);
        htParams.Add("@ErrorDetails", errorDesc);
        htParams.Add("@FunctionName", functionName);
        htParams.Add("@ClassName", className);
        htParams.Add("@ErrorGeneratedOn", DateTime.Now);  
        try
        {
            DatabaseOperation.ManageConnection(true);
            int noOfRowsAffected = DatabaseOperation.InsertUpdateDeleteOperation(htParams, Constants.SPLogError);            
        }
        catch (Exception ex)
        {
           
        }
        finally
        {
            DatabaseOperation.ManageConnection(false);
        }
    }
    static public class TopMostMessageBox
    {
        static public DialogResult Show(string message)
        {
            return Show(message, string.Empty, MessageBoxButtons.OK);
        }

        static public DialogResult Show(string message, string title)
        {
            return Show(message, title, MessageBoxButtons.OK);
        }

        static public DialogResult Show(string message, string title,
            MessageBoxButtons buttons)
        {
            // Create a host form that is a TopMost window which will be the 
            // parent of the MessageBox.
            Form topmostForm = new Form();
            // We do not want anyone to see this window so position it off the 
            // visible screen and make it as small as possible
            topmostForm.Size = new System.Drawing.Size(1, 1);
            topmostForm.StartPosition = FormStartPosition.Manual;
            System.Drawing.Rectangle rect = SystemInformation.VirtualScreen;
            topmostForm.Location = new System.Drawing.Point(rect.Bottom + 10,
                rect.Right + 10);
            topmostForm.Show();
            // Make this form the active form and make it TopMost
            topmostForm.Focus();
            topmostForm.BringToFront();
            topmostForm.TopMost = true;
            // Finally show the MessageBox with the form just created as its owner
            DialogResult result = MessageBox.Show(topmostForm, message, title,
                buttons);
            topmostForm.Dispose(); // clean it up all the way

            return result;
        }
    }

}