define([
    'app',
    'angular'
],function (app,angular) {
    app.service('GenerationUnitService',['$resource', '$q', 'Auth', '$location', '$translate', function ($resource, $q, Auth, $location, $translate) {

        function GenerationUnitService() {

            this.GenerationUnit = $resource('/Waste/GenerationUnitController/:operation', {}, {
                save:{
                    method:'POST',
                    params:{operation:'Save'}
                },
                query:{
                    method:'GET',
                    params:{operation:'SearchGUnit_Page'}

                },
                getGUnitData:{
                    method:'GET',
                    params:{operation:'GetGUnitDataByID'}
                },
                deleteGUnitID:{
                    method:'PUT',
                    params:{operation: 'DeleteGUnitByID'}
                }
            })
        }
        return new GenerationUnitService();
    }])

})