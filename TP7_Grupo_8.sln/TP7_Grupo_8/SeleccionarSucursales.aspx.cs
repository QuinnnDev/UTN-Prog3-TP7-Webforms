using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            lblErrorBusqueda.Text = string.Empty;
            lblErrorSeleccion.Text = string.Empty;

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

                ///GUARDAR LAS VARIABLES EN SESSION (SI NO FUE SELECCIONADA ANTES)
                bool existe = false;
                foreach (DataRow fila in tabla.Rows)
                {
                    if (fila["ID_SUCURSAL"].ToString() == IdSucursal)
                    {
                        existe = true;
                        break;
                    }
                }
                if (existe)
                {
                    lblErrorSeleccion.Text = "La sucursal ya fue seleccionada previamente.";
                    return;
                }

                DataRow nuevaFila = tabla.NewRow();
                nuevaFila["ID_SUCURSAL"] = IdSucursal;
                nuevaFila["NOMBRE"] = NombreProducto;
                nuevaFila["DESCRIPCION"] = Descripcion;
                tabla.Rows.Add(nuevaFila);

                Session["Seleccionado"] = tabla;
                lblErrorSeleccion.Text = string.Empty;

            }
        }

        protected void btnBuscarNombre_Click(object sender, EventArgs e)
        {
            GestionSucursales gestionSucursales = new GestionSucursales();
            if (!string.IsNullOrEmpty(txtBuscarNombre.Text))
            {
                lblErrorBusqueda.Text = string.Empty;
                lvSucursales.DataSource = gestionSucursales.ObtenerTabla("Sucursal", "SELECT Id_Sucursal, NombreSucursal, DescripcionSucursal, URL_Imagen_Sucursal FROM Sucursal WHERE NombreSucursal LIKE  '" + txtBuscarNombre.Text + "%'");
                lvSucursales.DataBind();
            }
            else
            {
                lblErrorBusqueda.Text = "*Por favor, ingrese un nombre de sucursal";
                lvSucursales.DataSource = gestionSucursales.ObtenerTodosLosDatos();
                lvSucursales.DataBind();
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idProvincia = btn.CommandArgument;
            GestionSucursales gestionSucu = new GestionSucursales();

            lvSucursales.DataSource = gestionSucu.ObtenerTabla("Sucursales", "SELECT Id_Sucursal, NombreSucursal, DescripcionSucursal, URL_Imagen_Sucursal FROM Sucursal WHERE Id_ProvinciaSucursal = " + idProvincia);
            lvSucursales.DataBind();

        }

        protected void lvSucursales_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager pager = (DataPager)lvSucursales.FindControl("DataPager1");
            pager.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            CargarListView();
        }
    }
}


