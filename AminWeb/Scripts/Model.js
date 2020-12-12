function GenerateProductCard( containerId) {
    const container = document.getElementById(containerId);


    container.innerHTML += 
        `
        <!-- #region Model -->

        <div class="model uk-border-rounded column uk-card uk-card-default uk-card-body uk-width-1-2@m">
            <img src="~/Resources/Raster/chair.png" alt="" />
            <content>
                <h3 class="uk-card-title"> ایپسوم متن ساختگی با تولید سادگی نامفهوم ا</h3>
                <p>
                    لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صو مجله در ستون و سطرآنچنان که لازم است
                    لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صو مجله در ستون و سطرآنچنان که لازم است
                </p>
                <div class="row star-container">
                    <div class="Stars" style="--rating: 2.5;" aria-label="Rating of this product is 2.3 out of 5."></div>
                </div>
                <div class="row">
                    <div class="column">
                        <div class="row">
                            <span class="uk-badge uk-margin-small-right">42%</span>
                            <label class="discount">25 000</label>
                        </div>
                        <div class="row">
                            <span class="price-toman">تومان </span>
                            <span class="price">25 000 000 </span>
                        </div>

                    </div>
                    <button class="btn-cart uk-border-rounded uk-button uk-button-primary">
                        <span uk-icon="icon: cart"></span>
                    </button>
                </div>
            </content>
        </div>

        <!-- #endregion -->

    `
}


GenerateProductCard('cards');