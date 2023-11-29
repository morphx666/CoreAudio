#region Copyright (c) 2002-2006 X-Component, All Rights Reserved
/* ---------------------------------------------------------------------*
*                           X-Component,                              *
*              Copyright (c) 2002-2006 All Rights reserved              *
*                                                                       *
*                                                                       *
* This file and its contents are protected by Vietnam and               *
* International copyright laws.  Unauthorized reproduction and/or       *
* distribution of all or any portion of the code contained herein       *
* is strictly prohibited and will result in severe civil and criminal   *
* penalties.  Any violations of this copyright will be prosecuted       *
* to the fullest extent possible under law.                             *
*                                                                       *
* THE SOURCE CODE CONTAINED HEREIN AND IN RELATED FILES IS PROVIDED     *
* TO THE REGISTERED DEVELOPER FOR THE PURPOSES OF EDUCATION AND         *
* TROUBLESHOOTING. UNDER NO CIRCUMSTANCES MAY ANY PORTION OF THE SOURCE *
* CODE BE DISTRIBUTED, DISCLOSED OR OTHERWISE MADE AVAILABLE TO ANY     *
* THIRD PARTY WITHOUT THE EXPRESS WRITTEN CONSENT OF ECONTECH JSC.,     *
*                                                                       *
* UNDER NO CIRCUMSTANCES MAY THE SOURCE CODE BE USED IN WHOLE OR IN     *
* PART, AS THE BASIS FOR CREATING A PRODUCT THAT PROVIDES THE SAME, OR  *
* SUBSTANTIALLY THE SAME, FUNCTIONALITY AS ANY ECONTECH JSC. PRODUCT.   *
*                                                                       *
* THE REGISTERED DEVELOPER ACKNOWLEDGES THAT THIS SOURCE CODE           *
* CONTAINS VALUABLE AND PROPRIETARY TRADE SECRETS OF ECONTECH JSC.,     *
* THE REGISTERED DEVELOPER AGREES TO EXPEND EVERY EFFORT TO             *
* INSURE ITS CONFIDENTIALITY.                                           *
*                                                                       *
* THE END USER LICENSE AGREEMENT (EULA) ACCOMPANYING THE PRODUCT        *
* PERMITS THE REGISTERED DEVELOPER TO REDISTRIBUTE THE PRODUCT IN       *
* EXECUTABLE FORM ONLY IN SUPPORT OF APPLICATIONS WRITTEN USING         *
* THE PRODUCT.  IT DOES NOT PROVIDE ANY RIGHTS REGARDING THE            *
* SOURCE CODE CONTAINED HEREIN.                                         *
*                                                                       *
* THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.              *
* --------------------------------------------------------------------- *
*/
#endregion Copyright (c) 2002-2006 X-Component, All Rights Reserved

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace XComponent.SliderBar
{
	/// <summary>
	/// Summary description for ColorHelper.
	/// </summary>
	internal class ColorHelper
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="red"></param>
		/// <param name="green"></param>
		/// <param name="blue"></param>
		/// <returns></returns>
		public static Color CreateColorFromRGB(int red, int green, int blue)
		{
			//Corect Red element
			int r = red;
			if (r > 255) 
			{
				r = 255;
			}
			if (r < 0) 
			{
				r = 0;
			}
			//Corect Green element
			int g = green;
			if (g > 255) 
			{
				g = 255;
			}
			if (g < 0) 
			{
				g = 0;
			}
			//Correct Blue Element
			int b = blue;
			if (b > 255) 
			{
				b = 255;
			}
			if (b < 0) 
			{
				b = 0;
			}
			return Color.FromArgb(r, g, b);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="blendColor"></param>
		/// <param name="baseColor"></param>
		/// <param name="opacity"></param>
		/// <returns></returns>
		public static Color OpacityMix(Color blendColor, Color baseColor, int opacity)
		{
			int r1;
			int g1;
			int b1;
			int r2;
			int g2;
			int b2;
			int r3;
			int g3;
			int b3;
			r1 = blendColor.R;
			g1 = blendColor.G;
			b1 = blendColor.B;
			r2 = baseColor.R;
			g2 = baseColor.G;
			b2 = baseColor.B;
			r3 = (int)(((r1 * ((float)opacity / 100)) + (r2 * (1 - ((float)opacity / 100)))));
			g3 = (int)(((g1 * ((float)opacity / 100)) + (g2 * (1 - ((float)opacity / 100)))));
			b3 = (int)(((b1 * ((float)opacity / 100)) + (b2 * (1 - ((float)opacity / 100)))));
			return CreateColorFromRGB(r3, g3, b3);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="baseColor"></param>
		/// <param name="blendColor"></param>
		/// <param name="opacity"></param>
		/// <returns></returns>
		public static Color SoftLightMix(Color baseColor, Color blendColor, int opacity)
		{
			int r1;
			int g1;
			int b1;
			int r2;
			int g2;
			int b2;
			int r3;
			int g3;
			int b3;
			r1 = baseColor.R;
			g1 = baseColor.G;
			b1 = baseColor.B;
			r2 = blendColor.R;
			g2 = blendColor.G;
			b2 = blendColor.B;
			r3 = SoftLightMath(r1, r2);
			g3 = SoftLightMath(g1, g2);
			b3 = SoftLightMath(b1, b2);
			return OpacityMix(CreateColorFromRGB(r3, g3, b3), baseColor, opacity);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="baseColor"></param>
		/// <param name="blendColor"></param>
		/// <param name="opacity"></param>
		/// <returns></returns>
		public static Color OverlayMix(Color baseColor, Color blendColor, int opacity)
		{
			int r1;
			int g1;
			int b1;
			int r2;
			int g2;
			int b2;
			int r3;
			int g3;
			int b3;
			r1 = baseColor.R;
			g1 = baseColor.G;
			b1 = baseColor.B;
			r2 = blendColor.R;
			g2 = blendColor.G;
			b2 = blendColor.B;
			r3 = OverlayMath(baseColor.R, blendColor.R);
			g3 = OverlayMath(baseColor.G, blendColor.G);
			b3 = OverlayMath(baseColor.B, blendColor.B);
			return OpacityMix(CreateColorFromRGB(r3, g3, b3), baseColor, opacity);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="ibase"></param>
		/// <param name="blend"></param>
		/// <returns></returns>
		private static int SoftLightMath(int ibase, int blend)
		{
			float dbase;
			float dblend;
			dbase = (float)ibase / 255;
			dblend = (float)blend / 255;
			if (dblend < 0.5) 
			{
				return (int)(((2 * dbase * dblend) + (Math.Pow(dbase, 2)) * (1 - (2 * dblend))) * 255);
			} 
			else 
			{
				return (int)(((Math.Sqrt(dbase) * (2 * dblend - 1)) + ((2 * dbase) * (1 - dblend))) * 255);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ibase"></param>
		/// <param name="blend"></param>
		/// <returns></returns>
		public static int OverlayMath(int ibase, int blend)
		{
			double dbase;
			double dblend;
			dbase = (double)ibase / 255;
			dblend = (double)blend / 255;
			if (dbase < 0.5) 
			{
				return (int)((2 * dbase * dblend) * 255);
			} 
			else 
			{
				return (int)((1 - (2 * (1 - dbase) * (1 - dblend))) * 255);
			}
		}

	}
}