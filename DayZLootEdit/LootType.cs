﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DayZLootEdit
{
    public class LootType
    {
        private XElement xtype;

        public string Name
        {
            get { return xtype.Attribute("name")?.Value; }
            set { xtype.Attribute("name").Value = value; }
        }

        public string Category
        {
            get { return xtype.Element("category")?.Attribute("name").Value; }
            set { xtype.Element("category")?.Attribute("name")?.SetValue(value); }
        }

        public int Nominal
        {
            get { return GetValueInt(xtype, "nominal"); }
            set { xtype.Element("nominal")?.SetValue(value.ToString()); }
        }

        public int Lifetime
        {
            get { return Lifetime = GetValueInt(xtype, "lifetime"); }
            set { xtype.Element("lifetime")?.SetValue(value.ToString()); }
        }
        public int Restock
        {
            get { return GetValueInt(xtype, "restock"); }
            set { xtype.Element("restock")?.SetValue(value.ToString()); }
        }
        public int Min
        {
            get { return GetValueInt(xtype, "min"); }
            set { xtype.Element("min")?.SetValue(value.ToString()); }
        }
        public int QuantMin
        {
            get { return GetValueInt(xtype, "quantmin"); }
            set { xtype.Element("quantmin")?.SetValue(value.ToString()); }
        }
        public int QuantMax
        {
            get { return GetValueInt(xtype, "quantmax"); }
            set { xtype.Element("quantmax")?.SetValue(value.ToString()); }
        }
        public int Cost
        {
            get { return GetValueInt(xtype, "cost"); }
            set { xtype.Element("cost")?.SetValue(value.ToString()); }
        }

        public bool CountInCargo
        {
            get { return GetFlag(xtype, "count_in_cargo"); }
            set { xtype.Element("flags")?.Attribute("count_in_cargo")?.SetValue(value.ToString()); }
        }
        public bool CountInHoarder
        {
            get { return GetFlag(xtype, "count_in_hoarder"); }
            set { xtype.Element("flags")?.Attribute("count_in_hoarder")?.SetValue(value.ToString()); }
        }
        public bool CountInMap
        {
            get { return GetFlag(xtype, "count_in_map"); }
            set { xtype.Element("flags")?.Attribute("count_in_map")?.SetValue(value.ToString()); }
        }
        public bool CountInPlayer
        {
            get { return GetFlag(xtype, "count_in_player"); }
            set { xtype.Element("flags")?.Attribute("count_in_player")?.SetValue(value.ToString()); }
        }

        public bool Crafted
        {
            get { return GetFlag(xtype, "crafted"); }
            set { xtype.Element("flags")?.Attribute("crafted")?.SetValue(value.ToString()); }
        }
        public bool Deloot
        {
            get { return GetFlag(xtype, "deloot"); }
            set { xtype.Element("flags")?.Attribute("deloot")?.SetValue(value.ToString()); }
        }

        public LootType(XElement xnode)
        {
            this.xtype = xnode;
        }

        public string Usage {
            get
            {
                return string.Join(", ",
                    xtype.Elements().Where(
                    node => node.Name.LocalName.Equals("usage")
                    ).Select(
                    node => node.Attribute("name")?.Value
                    ));
            }
            set {
                xtype.Elements().Where(node => node.Name.LocalName.Equals("usage")).Remove();

                foreach (string s in value.Split(',').Select(s => s.Trim()))
                {
                    xtype.Add(new XElement("usage", new XAttribute("name", s)));
                }
            }
        }

        public string Value
        {
            get
            {
                return string.Join(", ",
                    xtype.Elements().Where(
                    node => node.Name.LocalName.Equals("usage")
                    ).Select(
                    node => node.Attribute("name")?.Value
                    ));
            }
            set
            {
                xtype.Elements().Where(node => node.Name.LocalName.Equals("value")).Remove();

                foreach (string s in value.Split(',').Select(s => s.Trim()))
                {
                    xtype.Add(new XElement("value", new XAttribute("name", s)));
                }
            }
        }

        private int GetValueInt(XElement node, string name)
        {
            int val = 0;
            int.TryParse(node.Element(name)?.Value, out val);
            return val;
        }

        private bool GetFlag(XElement node, string attrib)
        {
            return (bool)node.Element("flags")?.Attribute(attrib)?.Value.Equals("1");
        }

        public void SetNominal(int percentage)
        {
            Nominal = (int) Math.Round(Nominal / 100.0 * percentage);
        }
        public void SetMin(int percentage)
        {
            Min = (int)Math.Round(Min / 100.0 * percentage);
        }

        public void RemoveType()
        {
            xtype.Remove();
        }

        /*
        <type name="AKM">
            <nominal>40</nominal>
            <lifetime>10800</lifetime>
            <restock>1800</restock>
            <min>20</min>
            <quantmin>-1</quantmin>
            <quantmax>-1</quantmax>
            <cost>100</cost>
            <flags count_in_cargo="1" count_in_hoarder="1" count_in_map="1" count_in_player="0" crafted="0" deloot="0"/>
            <category name="weapons"/>
            <usage name="Military"/>
        </type>
         */
    }
}
