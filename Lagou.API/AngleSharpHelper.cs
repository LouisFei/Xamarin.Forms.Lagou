﻿using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Lagou.API.Attributes;

namespace Lagou.API {
    public static class AngleSharpHelper {

        public static T Parse<T>(this HtmlParser parser, string source) where T : class, new() {
            var doc = parser.Parse(source);

            var t = new T();
            var ps = typeof(T).GetRuntimeProperties();
            #region
            foreach (var p in ps) {
                var attr = p.GetCustomAttribute<HtmlQueryAttribute>();
                if (attr != null) {
                    var ele = doc.QuerySelector(attr.Selector);
                    if (ele != null) {
                        string value = "";
                        switch (attr.ValueTarget) {
                            case HtmlQueryValueTargets.Attribute:
                                var a = ele.Attributes.GetNamedItem(attr.AttributeName);
                                if (a != null)
                                    value = a.Value;
                                break;
                            case HtmlQueryValueTargets.InnerHtml:
                                value = ele.InnerHtml;
                                break;
                            case HtmlQueryValueTargets.Text:
                                value = ele.TextContent;
                                break;
                        }

                        try {
                            if (!p.PropertyType.Equals(typeof(string))) {
                                var v = Convert.ChangeType(value, p.PropertyType);
                                p.SetValue(t, v);
                            } else {
                                p.SetValue(t, value);
                            }
                        } catch { }
                    }
                }
            }
            #endregion
            return t;
        }
    }
}