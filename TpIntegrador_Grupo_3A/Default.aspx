<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Default" %>

<asp:Content ID="BannerContent" ContentPlaceHolderID="BannerContent" runat="server">
    <!-- Carousel de imágenes -->
    <div id="carouselExample" class="carousel slide mb-4">
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img src="./Images/slider1.jpg" class="d-block w-100" style="height: 50vh; object-fit: cover;" alt="Imagen 1">
            </div>
            <div class="carousel-item">
                <img src="./Images/slider2.jpg" class="d-block w-100" style="height: 50vh; object-fit: cover;" alt="Imagen 2">
            </div>
            <div class="carousel-item">
                <img src="./Images/slider3.jpg" class="d-block w-100" style="height: 50vh; object-fit: cover;" alt="Imagen 3">
            </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExample" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselExample" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Next</span>
        </button>
    </div>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>

        <!-- Secciones de productos -->
        <div class="container m-5">
          <asp:Repeater ID="RptSecciones" runat="server" OnItemDataBound="RptSecciones_ItemDataBound">
    <ItemTemplate>
        <div class="row mb-4">
            <div class="card shadow-sm h-100">
                <div class="card-body text-center">
                    <h3 class="card-title"><%# Eval("Description") %></h3>
                    <p class="card-text">
                        <%# Eval("Description2") %>
                    </p>

                    <!-- Slider de Productos -->
                                <div class="row row-cols-1 row-cols-md-4 g-3">
                                    <!-- Repeater anidado para los productos -->
                                    <asp:Repeater ID="rptProdList" runat="server" OnItemCommand="rptProdList_ItemCommand">
    <ItemTemplate>
        <div class="col">
            <!-- LinkButton para hacer clic en la tarjeta completa -->
            <asp:LinkButton runat="server" CommandName="ViewProduct" CommandArgument='<%# Eval("Code") %>' CssClass="text-decoration-none">
                <div class="card h-100">
                    <div class="card-body">
                        <img style="height: 100px; object-fit: contain;"
                             src='<%# (Eval("Images") != null && ((List<Model.ImageProduct>)Eval("Images")).Count > 0) ? ((List<Model.ImageProduct>)Eval("Images"))[0].UrlImage : "https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg" %>' />

                        <p class="card-text d-block"><%# Eval("Description") %></p>
                        <asp:HiddenField ID="hfProdId" runat="server" Value='<%# Eval("Id") %>' />
                    </div>
                </div>
            </asp:LinkButton>
        </div>
    </ItemTemplate>
</asp:Repeater>

                                 </div>
                    <a href='/products?IdSection=<%# Eval("Id") %>' class="btn btn-primary mt-3">Ver todos</a>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
        

                 <%--seccion contacto--%>
         <section>
             <div id="contacto" class="scrool-section container-fluid">
                 <div class="row">
                     <div class="col-sm-12 col-md-6 col-lg-4 offset-lg-2">
                         <h2 class="text-center">Envianos un mensaje</h2>
                         <form class="row g-3 p-4 shadow-sm bg-white rounded" action="/" method="post" style="max-width: 600px; margin: auto;">
    <div class="col-md-6 mb-3">
        <label for="inputNombre" class="form-label visually-hidden">Nombre</label>
        <input
            type="text"
            class="form-control border-primary"
            id="inputNombre"
            placeholder="Nombre"
            required
            name="nombre" />
    </div>
    
    <div class="col-md-6 mb-3">
        <label for="inputAsunto" class="form-label visually-hidden">Asunto</label>
        <input
            type="text"
            class="form-control border-primary"
            id="inputAsunto"
            placeholder="Asunto"
            required
            name="asunto" />
    </div>
    
    <div class="col-12">
        <label for="inputEmail" class="form-label text-muted">Correo Electrónico</label>
        <input
            type="email"
            class="form-control border-primary"
            id="inputEmail"
            placeholder="ejemplo@email.com"
            required
            name="email" />
    </div>
    
    <div class="col-12">
        <label for="inputMensaje" class="form-label text-muted">Mensaje</label>
        <textarea
            class="form-control border-primary"
            id="inputMensaje"
            rows="4"
            placeholder="Escribe tu mensaje aquí..."
            required
            name="mensaje"></textarea>
    </div>
    
    <div class="col-12 d-grid mt-3">
        <button type="submit" class="btn btn-primary btn-lg w-50">Enviar Mensaje</button>
    </div>
</form>

                     </div>
                     <div class="col-sm-12 col-md-6 col-lg-4 offset-lg-1">
                         <h2 class="text-center">Conocé nuestro Local</h2>
                         <div class="mapa">
<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3289.8864863614817!2d-58.62594872364226!3d-34.455029273007995!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x95bca48d5b47bead%3A0xfe44d520c6eb8125!2sAv.%20Hip%C3%B3lito%20Yrigoyen%20288%2C%20B1617%20Gral.%20Pacheco%2C%20Provincia%20de%20Buenos%20Aires!5e0!3m2!1ses-419!2sar!4v1730736195988!5m2!1ses-419!2sar" width="300" height="250" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>                         </div>
                     </div>
                 </div>
             </div>

             <div class="subContacto row container-fluid">
                 <div class="col">
                     <h4 class="medios-contacto d-flex align-items-center">MAIL</h4>
                     <ul class="">
                         <li class="flex">
                             <div class="iconos-redes-sociales d-flex align-items-center">
                                 <a href="mailto:example@email.com" class=""><i
                                     class="bi bi-at"></i></a>UrbanGlam@email.com
                             </div>
                         </li>
                     </ul>
                 </div>
                 <div class="col">
                     <h4 class="medios-contacto">Telefonos</h4>
                     <ul class="">
                         <li class="flex">
                             <div class="iconos-redes-sociales d-flex align-items-center">
                                 <a><i class="bi bi-telephone"></i></a>
                                 011 5555 5555 / 9999
                             </div>
                         </li>
                         <li class="flex">
                             <div class="iconos-redes-sociales d-flex align-items-center">
                                 <a href="https://api.whatsapp.com/send?phone=9115555555">
                                     <i class="bi bi-whatsapp"></i>
                                 </a>
                                 WhatsApp
                             </div>
                         </li>
                     </ul>
                 </div>
                 <div id="redes-sociales" class="col">
                     <h4 class="medios-contacto scrool-section">Redes sociales</h4>
                     <ul class="">
                         <li class="flex">
                             <div class="iconos-redes-sociales d-flex align-items-center">
                                 <a href="https://www.instagram.com/" class="">
                                     <i class="bi bi-instagram"></i>
                                 </a>Instagram
                             </div>
                         </li>
                         <li class="flex">
                             <div class="iconos-redes-sociales d-flex align-items-center">
                                 <a href="https://www.facebook.com/" class="">
                                     <i class="bi bi-facebook"></i>
                                 </a>Facebook
                             </div>
                         </li>
                     </ul>
                 </div>
                 <div class="col">
                     <h4 class="medios-contacto">Direccion</h4>
                     <ul class="">
                         <li class="flex">
                             <div class="iconos-redes-sociales d-flex align-items-center">
                                 <a
                                     href="https://www.google.com.ar/maps/place/Av.+Hip%C3%B3lito+Yrigoyen+288,+B1617+Gral.+Pacheco,+Provincia+de+Buenos+Aires/@-34.4550293,-58.6259487,17z/data=!3m1!4b1!4m6!3m5!1s0x95bca48d5b47bead:0xfe44d520c6eb8125!8m2!3d-34.4550293!4d-58.6233738!16s%2Fg%2F11cscs2dxl?entry=ttu&g_ep=EgoyMDI0MTAyOS4wIKXMDSoASAFQAw%3D%3D">
                                     <i class="bi bi-geo-alt"></i>
                                 </a>
                                 Av. Hipólito Yrigoyen 288, B1617 Gral. Pacheco, Provincia de Buenos Aires
                             </div>
                         </li>
                     </ul>
                 </div>
             </div>
         </section>
         <%--fin seccion contacto--%>
            </div>
    </main>
</asp:Content>
