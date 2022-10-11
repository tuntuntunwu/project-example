
#region Copyright SHARP Corporation
//	Copyright (c) 2010-2014 SHARP CORPORATION. All rights reserved.
//
//	Extended Sharp OSA SDK
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER ``AS IS'' AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
//==============================================================================
#endregion
using CommonOSA;

public class OSACopy
{
    // Const Definition
    public class PropName : CommonConst
    {
        public static PropNameValue ROTATION = new PropNameValue("Rotation", "ROTATION");
        public static PropNameValue ORIGINAL_SIZE = new PropNameValue("OriginalSize", "ORIGINAL_SIZE");
        public static PropNameValue INPUT_TRAY = new PropNameValue("InputTray", "INPUT_TRAY");
        public static PropNameValue OUTPUT_TRAY = new PropNameValue("OutputTray", "OUTPUT_TRAY");

        public static PropNameValue FEEDER_RESOLUTION = new PropNameValue("FeederResolution", "FEEDER_RESOLUTION");
        public static PropNameValue PLATEN_RESOLUTION = new PropNameValue("PlatenResolution", "PLATEN_RESOLUTION");

        public static PropNameValue COLOR_MODE = new PropNameValue("ColorMode", "COLOR_MODE");
        public static PropNameValue COLOR_SELECT = new PropNameValue("ColorSelect", "COLOR_SELECT");
        public static PropNameValue COLOR_REPLACE = new PropNameValue("ColorReplace", "COLOR_REPLACE");
        public static PropNameValue EXPOSURE_LEVEL = new PropNameValue("ExposureLevel", "EXPOSURE_LEVEL");
        public static PropNameValue EXPOSURE_MODE = new PropNameValue("ExposureMode", "EXPOSURE_MODE");
        public static PropNameValue DUPLEX_MODE = new PropNameValue("DuplexMode", "DUPLEX_MODE");
        public static PropNameValue DUPLEX_BINDING_CHANGE = new PropNameValue("DuplexBindingChange", "DUPLEX_BINDING_CHANGE");

        public static PropNameValue TONER_SAVE = new PropNameValue("TonerSave", "TONER_SAVE");
        public static PropNameValue FOLD = new PropNameValue("Fold", "FOLD");

        public static PropNameValue COLLATE = new PropNameValue("Collate", "COLLATE");
        public static PropNameValue STAPLE = new PropNameValue("Staple", "STAPLE");
        public static PropNameValue OFFSET = new PropNameValue("Offset", "OFFSET");

        public static PropNameValue FILING = new PropNameValue("Filing", "FILING");
        public static PropNameValue FILE_PROPERTY = new PropNameValue("FileProperty", "FILE_PROPERTY");
        public static PropNameValue FILE_PASSWORD = new PropNameValue("FilePassword", "FILE_PASSWORD");
        public static string FILE_NAME = "FileName";
        public static string USER_NAME = "UserName";
        public static string PUBLIC_PDF = "PublicPDF";

        public static PropNameValue SADDLE_STITCH_BINDING = new PropNameValue("SaddleStitchBinding", "SADDLE_STITCH_BINDING");
        public static PropNameValue SADDLE_STITCH_ORIGINAL = new PropNameValue("SaddleStitchOriginal", "SADDLE_STITCH_ORIGINAL");
        public static string COVER = "Cover";
        public static PropNameValue COVER_INPUT_TRAY = new PropNameValue("CoverInputTray", "COVER_INPUT_TRAY");
        public static string COVER_TO_COPY = "CoverToCopy";

        public static PropNameValue SPECIAL_MODE = new PropNameValue("SpecialMode", "SPECIAL_MODE"); 
        
        public static PropNameValue MULTI_SHOT_MODE = new PropNameValue("MultiShotMode", "MULTI_SHOT_MODE");
        public static PropNameValue MULTI_SHOT_ORDER = new PropNameValue("MultiShotOrder", "MULTI_SHOT_ORDER");
        public static PropNameValue MULTI_SHOT_ORDER_2UP = new PropNameValue("MultiShotOrder", "MULTI_SHOT_ORDER_2UP");
        public static PropNameValue MULTI_SHOT_BORDER = new PropNameValue("MultiShotBorder", "MULTI_SHOT_BORDER");

        public const string COPYRATIO = "CopyRatio";
        public const string COPIES = "Copies";
        public const string PREVIEW = "Preview";
        public const string TONOR_SAVE = "TonerSave";
        public const string MANUAL_SIZE_X = "ManualSizeX";
        public const string MANUAL_SIZE_Y = "ManualSizeY";
        public const string PUNCH = "Punch";
        public const string PUNCHHOLE = "PunchHole";

        public const int SPECIAL_MODE_MAX_LENGTH = 44;
    }

    public class PropValue
    {
        public const string TRUE = "true";
        public const string FALSE = "false";

        public class ORIGINAL_SIZE
        {
            public const string AUTO = "auto";
            public const string MANUAL = "manual";
        }

        public class INPUT_TRAY
        {
            public const string AUTO = "auto";
            public const string TRAY1 = "tray1";
        }

        public class OUTPUT_TRAY
        {
            public const string AUTO = "auto";
            public const string OUTTRAY1 = "outtray1";
            public const string OUTTRAY2 = "outtray2";
            public const string OUTTRAY3 = "outtray3";
            public const string OUTTRAY4 = "outtray4";
            public const string OUTTRAY5 = "outtray5";
        }

        public class FILING
        {
            public const string NONE = "none";
            public const string MAIN = "main";
            public const string QUICK = "quick";
            public const string USER = "user";
        }

        public class SPECIAL_MODE
        {
            public const string NONE = "none";
            public const string MULTI_SHOT = "multi_shot";
            public const string ERASE = "erase";
            public const string MULTI_PAGE_ENLARGEMENT = "multi_page_enlargement";
            public const string PHOTO_REPORT = "photo_repeat";
            public const string CARD_SHOT = "card_shot";
            public const string JOB_BUILD = "job_build";
            public const string JOB_BUILD_MIXED_SOURCE = "job_build_mixed_source";
            public const string PAMPHLET_COPY = "pamphlet_copy";
        }
        
        public class COLOR_MODE
        {
            public const string AUTO = "auto";
            public const string MONOCHROME = "monochrome";
            public const string SINGLE_COLOR = "singlecolor";
            public const string DUAL_COLOR = "dualcolor";
            public const string FULL_COLOR = "fullcolor";
        }

        public class COLLATE
        {
            public const string AUTO = "auto";
            public const string SORT = "sort";
            public const string GROUP = "group";
        }

        public class STAPLE
        {
            public const string NONE = "none";
            public const string STAPLE_BACK = "1staple_back";
            public const string STAPLE_FRONT = "1staple_front";
            public const string STAPLES = "2staples";
            public const string SADDLE_STITCH = "saddle_stitch";
        }

        public class SADDLE_STITCH_BINDING
        {
            public const string NONE = "none";
            public const string LEFT = "left";
            public const string RIGHT = "right";
        }

        public class SADDLE_STITCH_ORIGINAL
        {
            public const string _1SIDED = "1sided";
            public const string _2SIDED = "2dided";
        }

        public class MULTI_SHOT_MODE
        {
            public const string TWO_UP = "2up";     // default
            public const string FOUR_UP = "4up";
            public const string EIGHT_UP = "8up";
        }
        public class MULTI_SHOT_ORDER
        {
            public const string Z = "z";            // default
            public const string R_Z = "reverse_z";
            public const string N = "n";
            public const string R_N = "reverse_n";
        }
        public class MULTI_SHOT_BORDER
        {
            public const string SOLID = "solid";    // default
            public const string DOTTED = "dotted";
            public const string NONE = "none";
        }
        public class EXPOSURE_LEVEL
        {
            public const string MIN_VALUE = "minValue";
            public const string MAX_VALUE = "maxValue";
        }
        public class FILE_PROPERTY
        {
            public const string SHARING = "0";
            public const string PROTECT = "1";
            public const string CONFIDENTIAL = "2";

        }
        public class FILE_PASSWORD
        {
            public const string MIN_LENGTH = "minLength";
            public const string MAX_LENGTH = "maxLength";
        }
        public class COPIES
        {
            public const string MIN_VALUE = "minValue";
            public const string MAX_VALUE = "maxValue";
        }
        public class COPYRATIO
        {
            public const string MIN_VALUE = "minValue";
            public const string MAX_VALUE = "maxValue";
        }
        public class MANUAL_SIZE_X
        {
            public const string MIN_VALUE = "minValue";
            public const string MAX_VALUE = "maxValue";
        }
        public class MANUAL_SIZE_Y
        {
            public const string MIN_VALUE = "minValue";
            public const string MAX_VALUE = "maxValue";
        }
    }
}