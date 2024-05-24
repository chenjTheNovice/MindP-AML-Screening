using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace AMLScreeningWpfApp1
{
    public class OFACXmlReader
    {
        public List<OFACEntity> ReadXml(string xmlUrl)
        {
            List<OFACEntity> ofacEntities = new List<OFACEntity>();

            try
            {
                XmlReader reader = XmlReader.Create(xmlUrl);
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(reader.NameTable);
                nsmgr.AddNamespace("ofac", "(link unavailable)");

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        OFACEntity entity = null;

                        switch (reader.Name)
                        {
                            case "ofac:sdnEntry":
                                // Create a new OFACEntity object
                                entity = new OFACEntity();

                                // Extract the data from the sdnEntry element
                                reader.ReadStartElement("ofac:sdnEntry");
                                string innerXml = reader.ReadInnerXml();

                                // Parse the inner XML to extract the child elements
                                XDocument doc = XDocument.Parse(innerXml);
                                entity.FirstName = doc.Root.Element("ns1:firstName")?.Value;
                                entity.Title = doc.Root.Element("ns1:title")?.Value;
                                entity.Remarks = doc.Root.Element("ns1:remarks")?.Value;

                                break;

                            case "SDN_ProgramList":
                                if (entity != null)
                                {
                                    // Extract the data from the SDN_ProgramList element
                                    reader.ReadStartElement("SDN_ProgramList");
                                    string programList = reader.ReadElementString("programList");

                                    // Add the program list to the entity
                                    entity.ProgramList = programList;
                                }
                                break;

                            case "Address":
                                if (entity != null)
                                {
                                    entity.AddressInfo = new Address();
                                    reader.ReadStartElement("Address");
                                    entity.AddressInfo.Uid = reader.ReadElementString("uid");
                                    entity.AddressInfo.Address1 = reader.ReadElementString("address1");
                                    entity.AddressInfo.Address2 = reader.ReadElementString("address2");
                                    entity.AddressInfo.Address3 = reader.ReadElementString("address3");
                                    entity.AddressInfo.City = reader.ReadElementString("city");
                                    entity.AddressInfo.State = reader.ReadElementString("state");
                                    entity.AddressInfo.PostalCode = reader.ReadElementString("postal code");
                                }
                                break;

                            case "Date of Birth":
                                if (entity != null)
                                {
                                    entity.DateOfBirthInfo = new DateOfBirthItem();
                                    reader.ReadStartElement("dateOfBirthItem");
                                    entity.DateOfBirthInfo.Uid = reader.ReadElementString("uid");
                                    entity.DateOfBirthInfo.DateOfBirth = reader.ReadElementString("dateofbirth");
                                }
                                break;

                            case "Place of Birth":
                                if (entity != null)
                                {
                                    entity.PlaceOfBirthInfo = new PlaceOfBirthItem();
                                    reader.ReadStartElement("placeOfBirthItem");
                                    entity.PlaceOfBirthInfo.Uid = reader.ReadElementString("uid");
                                    entity.PlaceOfBirthInfo.PlaceOfBirth = reader.ReadElementString("placeofbirth");
                                }
                                break;

                            case "Citizenship":
                                if (entity != null)
                                {
                                    entity.CitizenshipInfo = new Citizenship();
                                    reader.ReadStartElement("citizenship");
                                    entity.CitizenshipInfo.Uid = reader.ReadElementString("uid");
                                    entity.CitizenshipInfo.CitizenshipCountry = reader.ReadElementString("citizenship");
                                }
                                break;

                            case "Nationality":
                                if (entity != null)
                                {
                                    entity.NationalityInfo = new Nationality();
                                    reader.ReadStartElement("nationality");
                                    entity.NationalityInfo.Uid = reader.ReadElementString("uid");
                                    entity.NationalityInfo.NationalityCountry = reader.ReadElementString("country");
                                }
                                break;

                            case "VesselInfo":
                                if (entity != null)
                                {
                                    entity.VesselInfoDetails = new VesselInfo();
                                    reader.ReadStartElement("vesselInfo");
                                    entity.VesselInfoDetails.CallSign = reader.ReadElementString("callSign");
                                    entity.VesselInfoDetails.VesselType = reader.ReadElementString("vesselType");
                                    entity.VesselInfoDetails.VesselFlag = reader.ReadElementString("vesselFlag");
                                    entity.VesselInfoDetails.VesselOwner = reader.ReadElementString("vesselOwner");
                                    entity.VesselInfoDetails.GrossRegisteredTonnage = reader.ReadElementString("grossRegisteredTonnage");
                                    entity.VesselInfoDetails.Tonnage = reader.ReadElementString("tonnage");
                                }
                                break;

                            default:
                                break;
                        }

                        // Add the entity to the list if it was created
                        if (entity != null)
                        {
                            ofacEntities.Add(entity);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Log the error or handle it appropriately
                return new List<OFACEntity>(); // Return an empty list
            }

            return ofacEntities; // Return the list
        }
    }
}






