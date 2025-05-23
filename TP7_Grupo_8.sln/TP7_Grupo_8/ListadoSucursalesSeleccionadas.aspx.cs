﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP7_Grupo_8
{
    public partial class ListadoSucursalesSeleccionadas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack) 
            {
                if (Session["Seleccionado"] != null)
                {
                    gvSucursalesSeleccionadas.DataSource = Session["Seleccionado"];
                    gvSucursalesSeleccionadas.DataBind();
                }
                else
                {
                    lblMensaje.Text = "No hay datos de sucursales seleccionadas";
                }
            }
        }


    }
}