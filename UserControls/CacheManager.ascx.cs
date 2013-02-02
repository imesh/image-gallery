/*
 * Textpencil.Textpencil.ImageGallery 
 * An ASP.NET control for publishing Image Galleries on the web.
 * Copyright (C) 2008 Imesh Gunaratne
 * http://code.google.com/p/imagegallery
 * 
 * This file is part of Textpencil.Textpencil.ImageGallery.
 *
 * Textpencil.Textpencil.ImageGallery is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Textpencil.Textpencil.ImageGallery is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with Textpencil.Textpencil.ImageGallery.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * History: 2008/10/07 Created.
 *          
 */

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Textpencil.ImageGallery.UserControls
{
    public partial class CacheManager : System.Web.UI.UserControl
    {
        #region Private Attributes
        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {                
                InitializeControls();
                ResetErrorLabel();
                SetPageTitle();                                
            }
            catch (Exception e1)
            {
                ShowError(e1);
            }
        }
        #endregion

        #region Private Methods
        private void SetPageTitle()
        {
            Page.Title = "Image Gallery | Cache | " + DefaultSettings.SiteTitle;
            if (this.lblPageTitle != null)
                this.lblPageTitle.Text = "Image Gallery Cache";
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this.txtSecretKey != null)
            {
                if ((this.txtSecretKey.Text != null) && (this.txtSecretKey.Text.Equals(DefaultSettings.CacheSecretKey)))
                {
                    ThreadManager.UpdateFlickrImageCache();
                    ShowMessage("Refreshing Image Gallery Cache... This process will take few minutes. Thank you!");
                }
                else
                {
                    ShowMessage("The secret key typed is incorrect! Please retype the secret key and try again.", Color.Maroon);
                }
            }
        }

        private void ShowMessage(string message)
        {
            ShowMessage(message, Color.Black);
        }

        private void ShowMessage(string message, Color color)
        {
            if (this.lblMessage != null)
            {
                this.lblMessage.Text = message;
                this.lblMessage.ForeColor = color;
            }
        }

        private void InitializeControls()
        {            
        }

        private void ResetErrorLabel()
        {
            if (this.lblErrorDesc != null)
            {
                this.lblErrorDesc.Visible = false;
                this.lblErrorDesc.Text = String.Empty;
            }
        }

        private void ShowError(Exception e)
        {
            if (this.lblErrorDesc != null)
            {
                this.lblErrorDesc.Text = DefaultSettings.UnknownError;
                this.lblErrorDesc.Visible = true;
            }
        }
        #endregion
    }
}
