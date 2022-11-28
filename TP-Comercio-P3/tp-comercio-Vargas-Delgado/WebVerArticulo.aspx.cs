﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace tp_comercio_Vargas_Delgado
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ListaArticulo = negocio.listar();
            Session.Add("ListaArticulo", ListaArticulo);

            if (!IsPostBack)
            {
                //Queda inactivado porque usamos GridView en vez de tarjetas
                //Repeater1.DataSource = ListaArticulo;
                //Repeater1.DataBind();

                dgvArticulo.DataSource = ListaArticulo;
                dgvArticulo.DataBind();
            }
            if (Session["usuariologueado"] == null)
            {
                Session.Add("Error de acceso", "Debe iniciar sesión para acceder a esta página");
                Response.Redirect("Logon.aspx");
            }
        }
        
//Queda inactivado porque usamos GridView en vez de tarjetas
        //protected void btnEliminar_Click(object sender, EventArgs e)
        //{
        //    int idArticulo = Convert.ToInt32(((Button)sender).CommandArgument);
        //    ArticuloNegocio negocio = new ArticuloNegocio();
        //    negocio.eliminar(idArticulo);
        //    Response.Redirect("WebVerArticulo.aspx");
        //}
        
        protected void listaFiltrada()
        {
            string filtro = txtFiltro.Text;
            ListaArticulo = (List<Articulo>)Session["ListaArticulo"];
            ListaArticulo = ListaArticulo.FindAll(x => x.Nombre.Contains(filtro));

            // Queda inactivado porque usamos GridView en vez de tarjetas

            //Repeater1.DataSource = ListaArticulo;
            //Repeater1.DataBind();

            dgvArticulo.DataSource = ListaArticulo;
            dgvArticulo.DataBind();
        }
        protected void btnFiltro_Click(object sender, EventArgs e)
        {
            if (txtFiltro.Text != "")
            {
                listaFiltrada();
            }
            else
            {
                ListaArticulo = (List<Articulo>)Session["ListaArticulo"];

                // Queda inactivado porque usamos GridView en vez de tarjetas

                //Repeater1.DataSource = ListaArticulo;
                //Repeater1.DataBind();

                dgvArticulo.DataSource = ListaArticulo;
                dgvArticulo.DataBind();
            }
        }
        //protected void btnEditar_Click(object sender, EventArgs e)
        //{
        //    int IdArticulo = Convert.ToInt32(((Button)sender).CommandArgument);
        //    ArticuloNegocio negocio = new ArticuloNegocio();
        //    Articulo selecionado = (negocio.listaParaEditar(IdArticulo))[0];
        //    Session.Add("ArticuloSeleccionado", selecionado);
        //    Response.Redirect("FormularioArticulo.aspx");
        //}

        protected void dgv_SelectedIndexChanged(object sender, EventArgs e)
        {
            // int IdProveedor = Convert.ToInt32(((Button)sender).CommandArgument);
            //List<Proveedor> lista = negocio.ListaParaEditar(IdProveedor);

            int IdArticulo = Convert.ToInt32(dgvArticulo.SelectedDataKey.Value.ToString());
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo selecionado = (negocio.listaParaEditar(IdArticulo))[0];

            //enviar datos selecionado al formulario de proveedores
           
            Session.Add("ArticuloSeleccionado", selecionado);
            Response.Redirect("FormularioArticulo.aspx");

        }
        
    }
}