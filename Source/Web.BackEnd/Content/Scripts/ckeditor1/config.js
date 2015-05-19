/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';

    CKEDITOR.replace("ckecontent",
              {
                  filebrowserBrowseUrl: 'Content/Scripts/ckfinder/ckfinder.html',
                  filebrowserImageBrowseUrl: 'Content/Scripts/ckfinder/ckfinder.html?type=Images',
                  filebrowserFlashBrowseUrl: 'Content/Scripts/ckfinder/ckfinder.html?type=Flash',
                  filebrowserUploadUrl: 'Content/Scripts/ckfinder/core/connector/java/connector.java?command=QuickUpload&type=Files',
                  filebrowserImageUploadUrl: 'Content/Scripts/ckfinder/core/connector/java/connector.java?command=QuickUpload&type=Images',
                  filebrowserFlashUploadUrl: 'Content/Scripts/ckfinder/core/connector/java/connector.java?command=QuickUpload&type=Flash'
              });
};
