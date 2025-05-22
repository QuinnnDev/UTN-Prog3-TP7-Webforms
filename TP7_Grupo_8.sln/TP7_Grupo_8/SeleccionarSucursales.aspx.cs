using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TP7_Grupo_8.Conexion;

namespace TP7_Grupo_8
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarListView();
            }
        }

        
        private void CargarListView()
        {
            GestionSucursales gestionSucu = new GestionSucursales();
            lvSucursales.DataSource = gestionSucu.ObtenerTodosLosDatos();
            lvSucursales.DataBind();
        }

        protected void BtnSeleccionar_Command(object sender, CommandEventArgs e)
        {
            if(e.CommandName == "eventoSeleccionar")
            {
                DataTable tabla;
                if (Session["Seleccionado"] == null)
                {
                    tabla = new DataTable();
                    tabla.Columns.Add("ID_SUCURSAL");
                    tabla.Columns.Add("NOMBRE");
                    tabla.Columns.Add("DESCRIPCION");
                }
                else
                {
                    tabla = (DataTable)Session["Seleccionado"];
                }

                ///GUARDAR SELECCIONADO EN VARIABLES

                ///command argument: 
                ///                     Eval("Id_SucursalLabel") + "-" + Eval("NombreSucursalLabel") + "-" + Eval("DescripcionSucursalLabel")

                string datos = e.CommandArgument.ToString();

                string IdSucursal =     datos.Split('-')[0];
                string NombreProducto = datos.Split('-')[1];
                string Descripcion =    datos.Split('-')[2];
                
                ///GUARDAR LAS VARIABLES EN SESSION
                

                DataRow nuevaFila = tabla.NewRow();
                nuevaFila["ID_SUCURSAL"] = IdSucursal;
                nuevaFila["NOMBRE"] = NombreProducto;
                nuevaFila["DESCRIPCION"] = Descripcion;
                tabla.Rows.Add(nuevaFila);

                Session["Seleccionado"] = tabla;


            }
        }

        protected void btnBuscarNombre_Click(object sender, EventArgs e)
        {
            GestionSucursales gestionSucursales = new GestionSucursales();
            lvSucursales.DataSource = gestionSucursales.ObtenerTabla("Sucursal", "SELECT Id_Sucursal, NombreSucursal, DescripcionSucursal, URL_Imagen_Sucursal FROM Sucursal WHERE NombreSucursal LIKE  '" + txtBuscarNombre.Text + "%'");
            lvSucursales.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idProvincia = btn.CommandArgument;
            GestionSucursales gestionSucu = new GestionSucursales();

            lvSucursales.DataSource = gestionSucu.ObtenerTabla("Sucursales", "SELECT Id_Sucursal, NombreSucursal, DescripcionSucursal, URL_Imagen_Sucursal FROM Sucursal WHERE Id_ProvinciaSucursal = " + idProvincia);
            lvSucursales.DataBind();

        }
    }
}


