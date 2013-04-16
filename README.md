Image Gallery
---------------
An image gallery web control for publishing photo galleries on the web.
https://github.com/textpencil/image-gallery


License
--------
- Apache License Version 2.0
- http://www.apache.org/licenses/LICENSE-2.0


Credits
--------
- Flickr.NET API http://www.codeplex.com/FlickrNet
- Lightbox Slideshow http://www.justinbarkhuff.com/lab/lightbox_slideshow/

Functionality
--------------
- Display an Image Gallery in Thumbnail View or in Mosaic View.
- Slideshow images, click on an image once all the images are loaded.
- Read Image Information using a Flickr Account.
- Cache Image Information in the local Web Application to improve performance. 
  Use RefreshCache.aspx page to refresh the Image Cache. The default Secret Key is 'key'.

Requirements
-------------
- Mono XSP Web Server.
- A Flickr Account, Flickr API Key and Flickr User ID.
- As a support for the future development of the ImageGallery please keep the link https://github.com/textpencil/image-gallery to the project site in the footer.

Installation
-------------
1. Copy the ImageGallery folder in ~/App_Code to your Web Project's App_Code folder.
2. Copy the Default theme in ~/App_Themes folder to your App_Themes folder or else include the content of the style sheet in your Web Project's theme.
3. Copy the folders lightbox_slideshow and UserControls to you Web Root.
4. Copy the file Global.asax to your Web Root or else merge it's content with your existing file. Please note that the code behind, Global.asax.cs is placed inside ~/App_Code/ImageGallery folder.
5. Copy the files ImageGallery.master, ImageGallery.master.cs, RefreshCache.aspx and RefreshCache.aspx.cs to your Web Root.
6. Add the FlickrNET configuration to your Web.config file. Have a look at the ~/Web.config file.
   <flickrNet cacheDisabled="true"/>
   ...
   <compilation debug="true">
      <assemblies>
         <add assembly="FlickrNet"></add>
      </assemblies>
   </compilation>
7. Place the ImageGallery ASP.NET User Control in a new ASP Page. Have a look at the ~/Default.aspx file.
   <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/imagegallery.master" CodeFile="Default.aspx.cs" Inherits="Default" Theme="Default" %>
   <%@ Register TagPrefix="uc" TagName="Gallery" Src="~/UserControls/Gallery.ascx" %>

   <asp:Content ID="ImageGallery" ContentPlaceHolderID="MainContent" runat="Server">
   <uc:Gallery runat="server"></uc:Gallery>
   </asp:Content>
8. Set Flickr Account settings and other ImageGallery settings in ~/App_Code/ImageGallery/DefaultSettings.cs file.
   private static string flickrApiKey = "Place the Flickr API Key here";
   private static string flickrUserId = "Place the Flickr User ID here";
   
