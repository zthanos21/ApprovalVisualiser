using ApprovalVisualiser.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalVisualiser.ViewModels
{
    public class ProcessAttributesVM : ObservableCollection<AvailableProperty>
    {


        public void InitializeData(string data)
        {

            //var template = System.IO.File.ReadAllText(richText);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(data);

            var root = (CompilationUnitSyntax)tree.GetRoot();

            var firstMember = root.Members[0];

            var helloWorldDeclaration = (NamespaceDeclarationSyntax)firstMember;

            ClassDeclarationSyntax programDeclaration = (ClassDeclarationSyntax)helloWorldDeclaration.Members[0];

            var className = programDeclaration.TryGetInferredMemberName();


            // _AvailableProperties = new ObservableCollection<AvailableProperty>();

            foreach (var m in programDeclaration.Members)
            {
                if (m is PropertyDeclarationSyntax)
                {
                    AddProperty(m);
                    Console.WriteLine($"{(m as PropertyDeclarationSyntax).Identifier.Text} {(m as PropertyDeclarationSyntax).Type}");
                }


                //m.ide
            }
        }


        private void AddProperty(MemberDeclarationSyntax m)
        {
            this.Add(new AvailableProperty
            {
                PropertyName = (m as PropertyDeclarationSyntax).Identifier.Text,
                PopertyType = (m as PropertyDeclarationSyntax).Type.ToString(),
                // TP = ((m as PropertyDeclarationSyntax).Type.GetType())
            });
        }

        private string SampleText(FieldType ft)
        {
            switch (ft)
            {
                case FieldType.String:
                    return "Sample";
                case FieldType.Date:
                    return "18-5-2018";
                case FieldType.ReferenceData:
                    return "RefData";
                case FieldType.Number:
                    return "99";
                case FieldType.Amount:
                    return "1.500,00";
                case FieldType.Custom:
                    return "custom";
                default:
                    return "";
            }

        }
        public ObservableCollection<KeyValues> GetDetailsSample() => this.Where(w => w.UseForDetails).Select(s => new KeyValues
        {
            Key = s.Description,

            Value = SampleText(s.FieldType)
        }).AsObservableCollection();
        public string GetSummaryText() => string.Join(", ", this.Where(w => w.UseForSummary).Select(s => SampleText(s.FieldType)));
    }
}
