﻿using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;

namespace Test;

public class GetProperties : AddInPlugin
{
    public override int Execute(params string[] parameters)
    {
        test();
       // getProperty();
        return 0;
    }

    void test()
    {
        Document doc = Autodesk.Navisworks.Api.Application.ActiveDocument;
        string temppath = Path.Combine(Path.GetTempPath(), "test.txt");
        using (StreamWriter st = new StreamWriter(temppath))
        {
            foreach (ModelItem oItem in doc.CurrentSelection.SelectedItems)

            {
                // each PropertyCategory

                foreach (PropertyCategory propertyCategory in oItem.PropertyCategories)

                {
                    st.Write("***Property Category  " +
                                "[Display Name]: " +
                                propertyCategory.DisplayName +
                                "[Internal Name]: " +
                                propertyCategory.Name + "****\n");


                    // each property

                    foreach (DataProperty dataProperty in propertyCategory.Properties)

                    {
                        // is a display string

                        if (dataProperty.Value.IsDisplayString)

                        {
                            st.Write("   [Display Name]: " +
                                        dataProperty.DisplayName +
                                        "[Internal Name]: " +
                                        dataProperty.Name +
                                        "[Value]: " +
                                        dataProperty.Value.ToString() +
                                        "***\n");
                        }

                        // is a date / time

                        if (dataProperty.Value.IsDateTime)

                        {
                            st.Write("   [Display Name]: " +
                                        dataProperty.DisplayName +
                                        "[Internal Name]: " +
                                        dataProperty.Name +
                                        "[Value]: " +
                                        dataProperty.Value.ToDateTime().ToShortTimeString() +
                                        "***\n");
                        }
                    }
                }
            }
            st.Close();
            Process.Start(temppath);
        }

    }


    void getProperty()

    {
        Document oDoc =
            Autodesk.Navisworks.Api.Application.ActiveDocument;

        // get the first item of the selection

        ModelItem oSelectedItem =
            oDoc.CurrentSelection.SelectedItems.ElementAt<ModelItem>(0);


        //get a property category by display name    

        PropertyCategory oPC_DWGHandle =
            oSelectedItem.PropertyCategories.FindCategoryByDisplayName("Entity Handle");


        //get a property by internal name

        PropertyCategory oPC_DWGHandle1 =
            oSelectedItem.PropertyCategories.FindCategoryByName(
                PropertyCategoryNames.AutoCadEntityHandle);


        //get a property by combined name

        PropertyCategory oPC_DWGHandle2 =
            oSelectedItem.PropertyCategories.FindCategoryByCombinedName(
                new NamedConstant(
                    PropertyCategoryNames.AutoCadEntityHandle,
                    "Entity Handle"));


        //get a property by display name

        //(property caterogy and property)

        DataProperty oDP_DWGHandle =
            oSelectedItem.PropertyCategories.FindPropertyByDisplayName
                ("Entity Handle", "Value");


        //get a property by internal name

        DataProperty oDP_DWGHandle1 =
            oSelectedItem.PropertyCategories.FindPropertyByName(
                PropertyCategoryNames.AutoCadEntityHandle,
                DataPropertyNames.AutoCadEntityHandleValue);


        //get a property by combined name

        DataProperty oDP_DWGHandle2 =
            oSelectedItem.PropertyCategories.FindPropertyByCombinedName(
                new NamedConstant(
                    PropertyCategoryNames.AutoCadEntityHandle,
                    "Entity Handle"),
                new NamedConstant(
                    DataPropertyNames.AutoCadEntityHandleValue,
                    "Value"));


        //display the value of the property. e.g. use one

        // DataProperty got above to access its value

        MessageBox.Show(oDP_DWGHandle.Value.ToString());
    }
}