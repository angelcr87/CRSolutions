// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


       $(document).ready(function () {
           /* $('#candidates').DataTable();*/
         new DataTable('#candidates', {
         pagingType: 'full_numbers'
         });

           $('label > input').attr('id', 'searchTable')
           $('#candidates_filter').css("visibility", "hidden");
           $("#searchTable").css("visibility", "hidden");


           $('#search').on('keyup', function () {
               $('#searchTable').val($(this).val());
               $('#searchTable').trigger('keyup');

           });       

           $('#IdCantidate').on('change', function () {

               if ($('#IdCantidate').val() != '') {
                   $('#Create').attr('disable')
               }
               else {
                   $('#Create').removeAttr('disable')
               }             

           }); 

           $("#limpiar").click(function () {
               $("input[type=text], input[type=file] ,textarea, input[type=datetime-local],#datalistOptions2, #IdRiskScore, #IdCantidate").val("");
               $('input[type="radio"]').prop('checked', false);

               DisabledButtonCreate();

           });


           //validar campos 

       });
           



     

