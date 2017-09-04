/***************************************************************************************
* Name: XMLUtil.cs
* Function:XML工具;
* 
* Version     Date                Name                            Change Content
* ────────────────────────────────────────────────────────────────────────────────
* V1.0.0    20170904    http://blog.csdn.net/husheng0
* 
* Copyright (c). All rights reserved.
* 
***************************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

public static class XMLUtil 
{
    /// <summary>
    /// 写入xml文件;
    /// </summary>
    /// <param name="xmlFilePath">xml文件路径</param>
    /// <param name="element">节点信息</param>
    public static void WriteXml(string xmlFilePath,Dictionary<string,List<string>> element)
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlNode node = xmlDoc.CreateXmlDeclaration(DateTime.Now.ToString(), "utf-8", "");
        xmlDoc.AppendChild(node);

        foreach (KeyValuePair<string,List<string>> temp in element)
        {
            XmlNode root = xmlDoc.CreateElement(temp.Key);
            xmlDoc.AppendChild(root);
            for (int i = 0; i < temp.Value.Count; i++)
            {
                CreateNode(xmlDoc, root, "DependentAsset" + i, temp.Value[i]);
            }
        }

        try
        {
            xmlDoc.Save(xmlFilePath);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }  
    }

    /// <summary>
    /// 添加子节点;
    /// </summary>
    /// <param name="xmlDoc">文件</param>
    /// <param name="parentNode">父节点</param>
    /// <param name="name">名字</param>
    /// <param name="value">值</param>
    public static void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
    {
        XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
        node.InnerText = value;
        parentNode.AppendChild(node);
    }
}
