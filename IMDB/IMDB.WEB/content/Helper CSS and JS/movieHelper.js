<script type="text/ng-template" id="myTooltipTemplate.html">
    <div style="width:1000px;">
        <img ng-src="{{movie.company.image}}" class="md-card-image" alt="image caption" style="max-height:100%;max-width:100%;">
            <div>
                <br />
                <p class="text-left" style="word-wrap:break-word"><span style="white-space:pre-line; max-width:50%;">{{ movie.company.description }}</span></p>
            </div>
    </div>
</script>