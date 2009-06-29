using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections;

namespace Bagge.Seti.WebSite.Controls
{
	public class IconButton: Button
	{
		public string IconUrl
		{
			get { return ViewState["IconUrl"] as string; }
			set { ViewState["IconUrl"] = value; }
		}

		public string IconSkinID
		{
			get { return ViewState["IconSkinId"] as string; }
			set { ViewState["IconSkinId"] = value; }
		}


		Image _icon;
		Literal _text;

		protected override void CreateChildControls()
		{
			base.CreateChildControls();

			_icon = new Image();
			_icon.SkinID = IconSkinID;

			_text = new Literal();
			_text.Text = Text;

			Controls.Add(_icon);
			Controls.Add(_text);
		}

		internal static string EnsureEndWithSemiColon(string value)
		{
			if (value != null)
			{
				int length = value.Length;
				if ((length > 0) && (value[length - 1] != ';'))
				{
					return (value + ";");
				}
			}
			return value;
		}


		internal static string MergeScript(string firstScript, string secondScript)
		{
			if (!string.IsNullOrEmpty(firstScript))
			{
				return (firstScript + secondScript);
			}
			if (secondScript.TrimStart(new char[0]).StartsWith("javascript:", StringComparison.Ordinal))
			{
				return secondScript;
			}
			return ("javascript:" + secondScript);
		}

 
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			bool useSubmitBehavior = this.UseSubmitBehavior;
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			
			PostBackOptions postBackOptions = this.GetPostBackOptions();
			string uniqueID = this.UniqueID;
			if ((uniqueID != null) && ((postBackOptions == null) || (postBackOptions.TargetControl == this)))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueID);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Value, this.Text);
			bool isEnabled = base.IsEnabled;
			string firstScript = string.Empty;
			if (isEnabled)
			{
				firstScript = EnsureEndWithSemiColon(this.OnClientClick);
				if (base.HasAttributes)
				{
					string str3 = base.Attributes["onclick"];
					if (str3 != null)
					{
						firstScript = firstScript + EnsureEndWithSemiColon(str3);
						base.Attributes.Remove("onclick");
					}
				}
				if (this.Page != null)
				{
					string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(postBackOptions, false);
					if (postBackEventReference != null)
					{
						firstScript = MergeScript(firstScript, postBackEventReference);
					}
				}
			}
			if (this.Page != null)
			{
				this.Page.ClientScript.RegisterForEventValidation(postBackOptions);
			}
			if (firstScript.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, firstScript);
			}
			if (this.Enabled && !isEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}


			if (this.ID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			
			if (this.ControlStyleCreated && !this.ControlStyle.IsEmpty)
			{
				this.ControlStyle.AddAttributesToRender(writer, this);
			}
			
			AttributeCollection attributes = this.Attributes;
			IEnumerator enumerator = attributes.Keys.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string current = (string)enumerator.Current;
				writer.AddAttribute(current, attributes[current]);
			}
		}

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);


			writer.RenderBeginTag(HtmlTextWriterTag.Button);

			RenderChildren(writer);

			writer.RenderEndTag();
		}
	}
}
